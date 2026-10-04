using _26_HoangBaThong_Assignment01_BackEnd.DAL.Context;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;

public class NewsArticleDAO
{
    private static NewsArticleDAO? instance;
    private static readonly object instanceLock = new object();
    private NewsArticleDAO() { }

    public static NewsArticleDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null) instance = new NewsArticleDAO(); return instance;
            }
        }
    }

    public async Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync()
    {
        using var context = new FUNewsManagementContext(); return await context.NewsArticles.ToListAsync();
    }
}

