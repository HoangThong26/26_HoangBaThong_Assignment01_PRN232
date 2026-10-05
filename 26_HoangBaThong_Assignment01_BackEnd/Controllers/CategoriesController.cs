using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.Controllers;

public class CategoriesController : ODataController
{
    private readonly ICategoryService _service;
    public CategoriesController(ICategoryService service) { _service = service; }
    [EnableQuery]
    public async Task<IActionResult> Get() => Ok(await _service.GetCategoriesAsync());
    [EnableQuery]
    public async Task<IActionResult> Get(short key) { var c = await _service.GetCategoryByIdAsync(key); if (c == null) return NotFound(); return Ok(c); }
    public async Task<IActionResult> Post([FromBody] Category category) { if (!ModelState.IsValid) return BadRequest(ModelState); await _service.AddCategoryAsync(category); return Created(category); }
    public async Task<IActionResult> Put(short key, [FromBody] Category category) { if (!ModelState.IsValid) return BadRequest(ModelState); if (key != category.CategoryId) return BadRequest(); await _service.UpdateCategoryAsync(category); return Updated(category); }
    public async Task<IActionResult> Delete(short key) { try { await _service.DeleteCategoryAsync(key); return NoContent(); } catch (System.Exception ex) { return BadRequest(ex.Message); } }
}
