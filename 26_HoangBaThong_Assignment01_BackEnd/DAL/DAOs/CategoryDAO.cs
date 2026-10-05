using _26_HoangBaThong_Assignment01_BackEnd.DAL.Context;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;

public class CategoryDAO
{
    private static CategoryDAO? instance;
    private static readonly object instanceLock = new object();
    private CategoryDAO() { }
    public static CategoryDAO Instance
    {
        get { lock (instanceLock) { if (instance == null) instance = new CategoryDAO(); return instance; } }
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        using var context = new FUNewsManagementContext();
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(short id)
    {
        using var context = new FUNewsManagementContext();
        return await context.Categories.FindAsync(id);
    }

    public async Task AddCategoryAsync(Category category) { using var context = new FUNewsManagementContext(); context.Categories.Add(category); await context.SaveChangesAsync(); }

    public async Task UpdateCategoryAsync(Category category)
    {
        using var context = new FUNewsManagementContext();
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(short id)
    {
        using var context = new FUNewsManagementContext();
        var c = await context.Categories.FindAsync(id);
        if (c != null)
        {
            context.Categories.Remove(c);
            await context.SaveChangesAsync();
        }
    }
}

