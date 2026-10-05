using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;

public interface INewsArticleRepository
{
    Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync();
    Task<NewsArticle?> GetNewsArticleByIdAsync(string id);
    Task AddNewsArticleAsync(NewsArticle article);
    Task UpdateNewsArticleAsync(NewsArticle article);
    Task DeleteNewsArticleAsync(string id);
}
