using _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Implements;

public class SystemAccountRepository : ISystemAccountRepository
{
    public Task<IEnumerable<SystemAccount>> GetAccountsAsync() => SystemAccountDAO.Instance.GetAccountsAsync();
    public Task<SystemAccount?> GetAccountByIdAsync(short id) => SystemAccountDAO.Instance.GetAccountByIdAsync(id);
    public Task AddAccountAsync(SystemAccount account) => SystemAccountDAO.Instance.AddAccountAsync(account);
    public Task UpdateAccountAsync(SystemAccount account) => SystemAccountDAO.Instance.UpdateAccountAsync(account);
    public Task DeleteAccountAsync(short id) => SystemAccountDAO.Instance.DeleteAccountAsync(id);
}
