using _26_HoangBaThong_Assignment01_BackEnd.DAL.Context;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
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
        get { lock (instanceLock) { if (instance == null) instance = new NewsArticleDAO(); return instance; } }
    }

    public async Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync() { using var context = new FUNewsManagementContext(); return await context.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy).Include(n => n.Tags).ToListAsync(); }
    public async Task<NewsArticle?> GetNewsArticleByIdAsync(string id) { using var context = new FUNewsManagementContext(); return await context.NewsArticles.Include(n => n.Tags).FirstOrDefaultAsync(n => n.NewsArticleId == id); }
    public async Task AddNewsArticleAsync(NewsArticle article) { using var context = new FUNewsManagementContext(); context.NewsArticles.Add(article); await context.SaveChangesAsync(); }
    public async Task UpdateNewsArticleAsync(NewsArticle article) { using var context = new FUNewsManagementContext(); context.NewsArticles.Update(article); await context.SaveChangesAsync(); }
    public async Task DeleteNewsArticleAsync(string id) { using var context = new FUNewsManagementContext(); var a = await context.NewsArticles.FindAsync(id); if (a != null) { context.NewsArticles.Remove(a); await context.SaveChangesAsync(); } }
}
