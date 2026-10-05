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
    public class SystemAccountsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public SystemAccountsController(IHttpClientFactory clientFactory) { _clientFactory = clientFactory; }

        private bool IsAdmin() => HttpContext.Session.GetString("Role") == "Admin";

        public async Task<IActionResult> Index(string searchTerm = "")
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");

            var client = _clientFactory.CreateClient("BackendApi");
            string url = "/odata/SystemAccounts";
            if (!string.IsNullOrEmpty(searchTerm))
            {
                url += $"?$filter=contains(tolower(AccountName), tolower('{searchTerm}')) or contains(tolower(AccountEmail), tolower('{searchTerm}'))";
            }

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var accounts = JsonSerializer.Deserialize<List<SystemAccount>>(doc.RootElement.GetProperty("value").GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.SearchTerm = searchTerm;
                return View(accounts);
            }
            ViewBag.SearchTerm = searchTerm;
            return View(new List<SystemAccount>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SystemAccount account)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var content = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
            await client.PostAsync("/odata/SystemAccounts", content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SystemAccount account)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var content = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
            await client.PutAsync($"/odata/SystemAccounts({account.AccountId})", content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(short id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var response = await client.DeleteAsync($"/odata/SystemAccounts({id})");
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                try 
                {
                    var doc = JsonDocument.Parse(err);
                    if (doc.RootElement.TryGetProperty("error", out var errorElement) && errorElement.TryGetProperty("message", out var msgElement))
                    {
                        err = msgElement.GetString();
                    }
                } 
                catch {}
                if(err.Contains("<html", System.StringComparison.OrdinalIgnoreCase)) err = "An unexpected error occurred.";
                TempData["Error"] = "Cannot delete: " + err;
            }
            return RedirectToAction("Index");
        }
    }
}
