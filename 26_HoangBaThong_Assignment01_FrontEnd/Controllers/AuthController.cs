using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _26_HoangBaThong_Assignment01_FrontEnd.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public AuthController(IHttpClientFactory clientFactory) { _clientFactory = clientFactory; }

        [HttpGet]
        public IActionResult Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var client = _clientFactory.CreateClient("BackendApi");
            var content = new StringContent(JsonSerializer.Serialize(new { Email = email, Password = password }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var role = doc.RootElement.GetProperty("role").GetString();
                HttpContext.Session.SetString("Role", role ?? "");
                if(doc.RootElement.TryGetProperty("id", out var idProp)) HttpContext.Session.SetString("AccountId", idProp.GetInt16().ToString());
                
                if (role == "Admin") return RedirectToAction("Index", "SystemAccounts");
                else if(role == "1" || role == "Staff") return RedirectToAction("Index", "NewsArticles"); else return RedirectToAction("Index", "Profile");
            }
            ViewBag.Error = "Invalid email or password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
