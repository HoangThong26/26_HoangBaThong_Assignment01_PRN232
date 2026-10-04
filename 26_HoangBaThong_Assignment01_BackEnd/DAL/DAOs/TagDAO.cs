using _26_HoangBaThong_Assignment01_BackEnd.DAL.Context;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;

public class TagDAO
{
    private static TagDAO? instance;
    private static readonly object instanceLock = new object();
    private TagDAO() { }

    public static TagDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null) instance = new TagDAO(); return instance;
            }
        }
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync()
    {
        using var context = new FUNewsManagementContext(); return await context.Tags.ToListAsync();
    }
}

