using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using _26_HoangBaThong_Assignment01_FrontEnd.Models;

namespace _26_HoangBaThong_Assignment01_FrontEnd.Controllers
{
    public class ReportController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public ReportController(IHttpClientFactory clientFactory) { _clientFactory = clientFactory; }

        public async Task<IActionResult> Index(string startDate, string endDate)
        {
            if (HttpContext.Session.GetString("Role") != "Admin") return RedirectToAction("Login", "Auth");

            var client = _clientFactory.CreateClient("BackendApi");
            string query = "/odata/NewsArticles?$orderby=CreatedDate desc";

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                query = $"/odata/NewsArticles?$filter=CreatedDate ge {startDate}T00:00:00Z and CreatedDate le {endDate}T23:59:59Z&$orderby=CreatedDate desc";
            }

            var response = await client.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var articles = JsonSerializer.Deserialize<List<NewsArticle>>(doc.RootElement.GetProperty("value").GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View(articles);
            }
            return View(new List<NewsArticle>());
        }
    }
}
