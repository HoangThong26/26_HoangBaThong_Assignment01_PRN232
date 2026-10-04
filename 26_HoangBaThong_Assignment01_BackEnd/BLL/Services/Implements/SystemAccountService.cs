using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Implements;

public class SystemAccountService : ISystemAccountService
{
    private readonly ISystemAccountRepository _repository;
    private readonly INewsArticleRepository _newsRepo;

    public SystemAccountService(ISystemAccountRepository repository, INewsArticleRepository newsRepo)
    {
        _repository = repository;
        _newsRepo = newsRepo;
    }

    public Task<IEnumerable<SystemAccount>> GetAccountsAsync() => _repository.GetAccountsAsync();
    public Task<SystemAccount?> GetAccountByIdAsync(short id) => _repository.GetAccountByIdAsync(id);
    public Task AddAccountAsync(SystemAccount account) => _repository.AddAccountAsync(account);
    public Task UpdateAccountAsync(SystemAccount account) => _repository.UpdateAccountAsync(account);
    public async Task DeleteAccountAsync(short id)
    {
        var articles = await _newsRepo.GetNewsArticlesAsync();
        bool hasArticles = false;
        foreach (var a in articles) { if (a.CreatedById == id) { hasArticles = true; break; } }
        if (hasArticles) throw new System.Exception("Cannot delete account because it has created news articles.");
        await _repository.DeleteAccountAsync(id);
    }
}
