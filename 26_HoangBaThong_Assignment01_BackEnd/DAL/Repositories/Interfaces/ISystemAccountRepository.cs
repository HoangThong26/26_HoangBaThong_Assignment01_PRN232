using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;

public interface ISystemAccountRepository
{
    Task<IEnumerable<SystemAccount>> GetAccountsAsync();
    Task<SystemAccount?> GetAccountByIdAsync(short id);
    Task AddAccountAsync(SystemAccount account);
    Task UpdateAccountAsync(SystemAccount account);
    Task DeleteAccountAsync(short id);
}
