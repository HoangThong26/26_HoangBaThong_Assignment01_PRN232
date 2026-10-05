using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;

namespace _26_HoangBaThong_Assignment01_BackEnd.Controllers;

public class SystemAccountsController : ODataController
{
    private readonly ISystemAccountService _service;

    public SystemAccountsController(ISystemAccountService service)
    {
        _service = service;
    }

    [EnableQuery]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAccountsAsync());
    }
    [EnableQuery]
    public async Task<IActionResult> Get(short key)
    {
        var account = await _service.GetAccountByIdAsync(key);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SystemAccount account)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _service.AddAccountAsync(account);
        return Created(account);
    }

    public async Task<IActionResult> Put(short key, [FromBody] SystemAccount account)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (key != account.AccountId) return BadRequest();
        await _service.UpdateAccountAsync(account);
        return Updated(account);
    }

    public async Task<IActionResult> Delete(short key)
    {
        try
        {
            await _service.DeleteAccountAsync(key);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
