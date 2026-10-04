using _26_HoangBaThong_Assignment01_BackEnd.DAL.Context;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;

public class SystemAccountDAO
{
    private static SystemAccountDAO? instance;
    private static readonly object instanceLock = new object();
    private SystemAccountDAO() { }

    public static SystemAccountDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null) instance = new SystemAccountDAO(); return instance;
            }
        }
    }

    public async Task<IEnumerable<SystemAccount>> GetAccountsAsync()
    {
        using var context = new FUNewsManagementContext(); return await context.SystemAccounts.ToListAsync();
    }
    public async Task<SystemAccount?> GetAccountByIdAsync(short id)
    {
        using var context = new FUNewsManagementContext();
        return await context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountId == id);
    }

    public async Task AddAccountAsync(SystemAccount account)
    {
        using var context = new FUNewsManagementContext();
        context.SystemAccounts.Add(account);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAccountAsync(SystemAccount account)
    {
        using var context = new FUNewsManagementContext();
        context.SystemAccounts.Update(account);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(short id)
    {
        using var context = new FUNewsManagementContext();
        var account = await context.SystemAccounts.FindAsync(id);
        if (account != null)
        {
            context.SystemAccounts.Remove(account);
            await context.SaveChangesAsync();
        }
    }
}
