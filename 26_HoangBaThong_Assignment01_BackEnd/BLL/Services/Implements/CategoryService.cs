using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Implements;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    private readonly INewsArticleRepository _newsRepo;
    public CategoryService(ICategoryRepository repo, INewsArticleRepository newsRepo) { _repo = repo; _newsRepo = newsRepo; }
    public Task<IEnumerable<Category>> GetCategoriesAsync() => _repo.GetCategoriesAsync();
    public Task<Category?> GetCategoryByIdAsync(short id) => _repo.GetCategoryByIdAsync(id);
    public async Task AddCategoryAsync(Category category) { await _repo.AddCategoryAsync(category); }
    public Task UpdateCategoryAsync(Category category) => _repo.UpdateCategoryAsync(category);
    public async Task DeleteCategoryAsync(short id)
    {
        var news = await _newsRepo.GetNewsArticlesAsync();
        if (news.Any(n => n.CategoryId == id)) throw new System.Exception("Cannot delete category because it has news articles.");
        var cats = await _repo.GetCategoriesAsync();
        if (cats.Any(c => c.ParentCategoryId == id)) throw new System.Exception("Cannot delete category because it is a parent to other categories.");
        await _repo.DeleteCategoryAsync(id);
    }
}


