using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using _26_HoangBaThong_Assignment01_FrontEnd.Models;

namespace _26_HoangBaThong_Assignment01_FrontEnd.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public NewsArticlesController(IHttpClientFactory clientFactory) { _clientFactory = clientFactory; }

        private bool IsStaff() => HttpContext.Session.GetString("Role") == "1" || HttpContext.Session.GetString("Role") == "Staff";

        private async Task LoadCategoriesAndTags()
        {
            var client = _clientFactory.CreateClient("BackendApi");
            var resCat = await client.GetAsync("/odata/Categories?$filter=IsActive eq true");
            if (resCat.IsSuccessStatusCode)
            {
                var doc = JsonDocument.Parse(await resCat.Content.ReadAsStringAsync());
                ViewBag.Categories = JsonSerializer.Deserialize<List<Category>>(doc.RootElement.GetProperty("value").GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            var resTag = await client.GetAsync("/odata/Tags");
            if (resTag.IsSuccessStatusCode)
            {
                var doc = JsonDocument.Parse(await resTag.Content.ReadAsStringAsync());
                ViewBag.Tags = JsonSerializer.Deserialize<List<Tag>>(doc.RootElement.GetProperty("value").GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<IActionResult> Index(string searchTerm = "")
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");
            await LoadCategoriesAndTags();

            var accountId = HttpContext.Session.GetString("AccountId");
            var client = _clientFactory.CreateClient("BackendApi");

            string filter = $"CreatedById eq {accountId}";
            if (!string.IsNullOrEmpty(searchTerm)) filter += $" and contains(tolower(NewsTitle), tolower('{searchTerm}'))";

            string url = $"/odata/NewsArticles?$expand=Category,CreatedBy,Tags&$filter={filter}";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var articles = JsonSerializer.Deserialize<List<NewsArticle>>(doc.RootElement.GetProperty("value").GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.SearchTerm = searchTerm;
                return View(articles);
            }
            return View(new List<NewsArticle>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsArticle article, List<int> TagIds)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");

            article.CreatedById = short.Parse(HttpContext.Session.GetString("AccountId"));
            article.CreatedDate = DateTime.Now;
            article.ModifiedDate = DateTime.Now;
            if (TagIds != null) article.Tags = TagIds.Select(id => new Tag { TagId = id }).ToList();

            var client = _clientFactory.CreateClient("BackendApi");
            var content = new StringContent(JsonSerializer.Serialize(article), Encoding.UTF8, "application/json");
            var res = await client.PostAsync("/odata/NewsArticles", content);

            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadAsStringAsync();
                TempData["Error"] = "Cannot create: " + err;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsArticle article, List<int> TagIds)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");

            article.UpdatedById = short.Parse(HttpContext.Session.GetString("AccountId"));
            article.ModifiedDate = DateTime.Now;
            if (TagIds != null) article.Tags = TagIds.Select(id => new Tag { TagId = id }).ToList();

            var client = _clientFactory.CreateClient("BackendApi");
            var getRes = await client.GetAsync($"/odata/NewsArticles('{article.NewsArticleId}')");
            if (getRes.IsSuccessStatusCode)
            {
                var existing = JsonSerializer.Deserialize<NewsArticle>(await getRes.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                article.CreatedById = existing.CreatedById;
                article.CreatedDate = existing.CreatedDate;
            }

            var content = new StringContent(JsonSerializer.Serialize(article), Encoding.UTF8, "application/json");
            await client.PutAsync($"/odata/NewsArticles('{article.NewsArticleId}')", content);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Auth");
            var client = _clientFactory.CreateClient("BackendApi");
            await client.DeleteAsync($"/odata/NewsArticles('{id}')");
            return RedirectToAction("Index");
        }
    }
}
