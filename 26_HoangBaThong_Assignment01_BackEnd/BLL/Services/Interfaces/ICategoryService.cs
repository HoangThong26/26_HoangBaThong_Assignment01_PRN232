using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(short id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(short id);
}
