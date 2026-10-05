using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using _26_HoangBaThong_Assignment01_FrontEnd.Models;
namespace _26_HoangBaThong_Assignment01_FrontEnd.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public CategoriesController(IHttpClientFactory clientFactory) { _clientFactory = clientFactory; }
        private bool IsStaff() => HttpContext.Session.GetString("Role") == "1" || HttpContext.Session.GetString("Role") == "Staff";

        public async Task<IActionResult> Index(string searchTerm = "")
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            string url = "/odata/Categories";
            if (!string.IsNullOrEmpty(searchTerm)) url += $"?$filter=contains(tolower(CategoryName), tolower('{searchTerm}'))";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var cats = JsonSerializer.Deserialize<List<Category>>(doc.RootElement.GetProperty("value").GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.SearchTerm = searchTerm;
                return View(cats);
            }
            return View(new List<Category>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
            await client.PostAsync("/odata/Categories", content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
            await client.PutAsync($"/odata/Categories({category.CategoryId})", content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(short id)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var response = await client.DeleteAsync($"/odata/Categories({id})");
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                try { var doc = JsonDocument.Parse(err); if (doc.RootElement.TryGetProperty("error", out var e) && e.TryGetProperty("message", out var m)) err = m.GetString() ?? err; } catch { }
                if (err != null && err.Contains("<html", System.StringComparison.OrdinalIgnoreCase)) err = "An unexpected error occurred.";
                TempData["Error"] = "Cannot delete: " + err;
            }
            return RedirectToAction("Index");
        }
    }
}
