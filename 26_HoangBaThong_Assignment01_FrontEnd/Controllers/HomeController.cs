using _26_HoangBaThong_Assignment01_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public HomeController(IHttpClientFactory clientFactory) { _clientFactory = clientFactory; }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("BackendApi");
            var response = await client.GetAsync("/odata/NewsArticles?$filter=NewsStatus eq true&$expand=Category,CreatedBy&$orderby=CreatedDate desc");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var news = JsonSerializer.Deserialize<List<NewsArticle>>(doc.RootElement.GetProperty("value").GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(news);
            }
            return View(new List<NewsArticle>());
        }

        public IActionResult Privacy() { return View(); }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() { return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); }
    }
}
