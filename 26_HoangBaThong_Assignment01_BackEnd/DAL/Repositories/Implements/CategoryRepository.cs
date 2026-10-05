using _26_HoangBaThong_Assignment01_BackEnd.DAL.DAOs;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Implements;

public class CategoryRepository : ICategoryRepository
{
    public Task<IEnumerable<Category>> GetCategoriesAsync() => CategoryDAO.Instance.GetCategoriesAsync();
    public Task<Category?> GetCategoryByIdAsync(short id) => CategoryDAO.Instance.GetCategoryByIdAsync(id);
    public Task AddCategoryAsync(Category category) => CategoryDAO.Instance.AddCategoryAsync(category);
    public Task UpdateCategoryAsync(Category category) => CategoryDAO.Instance.UpdateCategoryAsync(category);
    public Task DeleteCategoryAsync(short id) => CategoryDAO.Instance.DeleteCategoryAsync(id);
}
