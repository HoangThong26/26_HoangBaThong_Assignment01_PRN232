using _26_HoangBaThong_Assignment01_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text;
namespace _26_HoangBaThong_Assignment01_FrontEnd.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProfileController(IHttpClientFactory clientFactory) { _clientFactory = clientFactory; }

        public async Task<IActionResult> Index()
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            if (accountId == null) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var response = await client.GetAsync($"/odata/SystemAccounts({accountId})");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var account = JsonSerializer.Deserialize<SystemAccount>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(account);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SystemAccount account)
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            if (accountId == null || short.Parse(accountId) != account.AccountId) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            var content = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
            await client.PutAsync($"/odata/SystemAccounts({account.AccountId})", content);
            TempData["Message"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }
    }
}
