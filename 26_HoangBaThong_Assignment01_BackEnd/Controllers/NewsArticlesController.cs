using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;

namespace _26_HoangBaThong_Assignment01_BackEnd.Controllers;

public class NewsArticlesController : ODataController
{
    private readonly INewsArticleService _service;

    public NewsArticlesController(INewsArticleService service)
    {
        _service = service;
    }

    [EnableQuery]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetNewsArticlesAsync());
    }
}
