using _26_HoangBaThong_Assignment01_BackEnd.DAL.Context;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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

    public async Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync()
    {
        using var context = new FUNewsManagementContext();
        return await context.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy).Include(n => n.Tags).ToListAsync();
    }
    public async Task<NewsArticle?> GetNewsArticleByIdAsync(string id)
    {
        using var context = new FUNewsManagementContext();
        return await context.NewsArticles.Include(n => n.Tags).FirstOrDefaultAsync(n => n.NewsArticleId == id);
    }
    public async Task AddNewsArticleAsync(NewsArticle article)
    {
        using var context = new FUNewsManagementContext();
        var tagIds = article.Tags?.Select(t => t.TagId).ToList() ?? new List<int>();
        article.Tags?.Clear();
        foreach (var id in tagIds)
        {
            var t = await context.Tags.FindAsync(id);
            if (t != null) { article.Tags ??= new List<Tag>(); article.Tags.Add(t); }
        }
        context.NewsArticles.Add(article);
        await context.SaveChangesAsync();
    }
    public async Task UpdateNewsArticleAsync(NewsArticle article)
    {
        using var context = new FUNewsManagementContext();
        var existing = await context.NewsArticles.Include(n => n.Tags).FirstOrDefaultAsync(n => n.NewsArticleId == article.NewsArticleId);
        if (existing != null)
        {
            context.Entry(existing).CurrentValues.SetValues(article);
            existing.Tags.Clear();
            var tagIds = article.Tags?.Select(t => t.TagId).ToList() ?? new List<int>();
            foreach (var id in tagIds)
            {
                var t = await context.Tags.FindAsync(id);
                if (t != null) existing.Tags.Add(t);
            }
            await context.SaveChangesAsync();
        }
    }
    public async Task DeleteNewsArticleAsync(string id)
    {
        using var context = new FUNewsManagementContext();
        var a = await context.NewsArticles.FindAsync(id);
        if (a != null)
        {
            context.NewsArticles.Remove(a);
            await context.SaveChangesAsync();
        }
    }
}
