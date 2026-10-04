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

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [EnableQuery]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetCategoriesAsync());
    }
}
