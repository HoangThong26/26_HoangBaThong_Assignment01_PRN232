using _26_HoangBaThong_Assignment01_BackEnd.DAL.Context;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL;
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
        get
        {
            lock (instanceLock)
            {
                if (instance == null) instance = new CategoryDAO(); return instance;
            }
        }
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        using var context = new FUNewsManagementContext(); return await context.Categories.ToListAsync();
    }
}

