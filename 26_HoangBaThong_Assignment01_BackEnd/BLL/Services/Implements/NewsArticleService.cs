using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Implements;

public class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _repo;
    public NewsArticleService(INewsArticleRepository repo) { _repo = repo; }
    public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync() => _repo.GetNewsArticlesAsync();
    public Task<NewsArticle?> GetNewsArticleByIdAsync(string id) => _repo.GetNewsArticleByIdAsync(id);
    public async Task AddNewsArticleAsync(NewsArticle article) {
        article.CreatedDate = System.DateTime.Now;
        await _repo.AddNewsArticleAsync(article);
    }
    public async Task UpdateNewsArticleAsync(NewsArticle article) {
        article.ModifiedDate = System.DateTime.Now;
        await _repo.UpdateNewsArticleAsync(article);
    }
    public Task DeleteNewsArticleAsync(string id) => _repo.DeleteNewsArticleAsync(id);
}
