using _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Implements;

public class NewsArticleRepository : INewsArticleRepository
{
    public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync() => NewsArticleDAO.Instance.GetNewsArticlesAsync();
}

