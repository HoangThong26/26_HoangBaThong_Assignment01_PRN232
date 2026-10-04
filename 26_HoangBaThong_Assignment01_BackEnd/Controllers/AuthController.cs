using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using System.Linq;

namespace _26_HoangBaThong_Assignment01_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ISystemAccountService _accountService;

    public AuthController(IConfiguration config, ISystemAccountService accountService)
    {
        _config = config;
        _accountService = accountService;
    }

    public class LoginRequest { public string Email { get; set; } = null!; public string Password { get; set; } = null!; }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var adminEmail = _config["AdminAccount:Email"];
        var adminPass = _config["AdminAccount:Password"];

        if (request.Email == adminEmail && request.Password == adminPass)
        {
            return Ok(new { Role = "Admin", Email = request.Email });
        }

        var accounts = await _accountService.GetAccountsAsync();
        var user = accounts.FirstOrDefault(a => a.AccountEmail == request.Email && a.AccountPassword == request.Password);

        if (user != null)
        {
            return Ok(new { Role = user.AccountRole == 1 ? "Staff" : "Lecturer", Email = user.AccountEmail, Id = user.AccountId });
        }

        return Unauthorized("Invalid email or password.");
    }
}
