using _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Implements;

public class NewsArticleRepository : INewsArticleRepository
{
    public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync() => NewsArticleDAO.Instance.GetNewsArticlesAsync();
    public Task<NewsArticle?> GetNewsArticleByIdAsync(string id) => NewsArticleDAO.Instance.GetNewsArticleByIdAsync(id);
    public Task AddNewsArticleAsync(NewsArticle article) => NewsArticleDAO.Instance.AddNewsArticleAsync(article);
    public Task UpdateNewsArticleAsync(NewsArticle article) => NewsArticleDAO.Instance.UpdateNewsArticleAsync(article);
    public Task DeleteNewsArticleAsync(string id) => NewsArticleDAO.Instance.DeleteNewsArticleAsync(id);
}
