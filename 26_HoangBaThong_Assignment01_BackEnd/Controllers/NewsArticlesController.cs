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
    public NewsArticlesController(INewsArticleService service) { _service = service; }
    [EnableQuery]
    public async Task<IActionResult> Get() => Ok(await _service.GetNewsArticlesAsync());
    [EnableQuery]
    public async Task<IActionResult> Get(string key) { var n = await _service.GetNewsArticleByIdAsync(key); if (n == null) return NotFound(); return Ok(n); }
    public async Task<IActionResult> Post([FromBody] NewsArticle article) { if (!ModelState.IsValid) return BadRequest(ModelState); await _service.AddNewsArticleAsync(article); return Created(article); }
    public async Task<IActionResult> Put(string key, [FromBody] NewsArticle article) { if (!ModelState.IsValid) return BadRequest(ModelState); if (key != article.NewsArticleId) return BadRequest(); await _service.UpdateNewsArticleAsync(article); return Updated(article); }
    public async Task<IActionResult> Delete(string key) { try { await _service.DeleteNewsArticleAsync(key); return NoContent(); } catch (System.Exception ex) { return BadRequest(ex.Message); } }
}
