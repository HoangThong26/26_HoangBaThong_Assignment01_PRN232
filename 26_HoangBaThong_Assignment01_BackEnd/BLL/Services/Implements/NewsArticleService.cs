using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Implements;

public class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _repository;
    public NewsArticleService(INewsArticleRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync() => _repository.GetNewsArticlesAsync();
}

