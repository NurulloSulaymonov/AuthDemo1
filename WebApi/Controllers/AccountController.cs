using System.Net;
using Domain.Dtos;
using Domain.Response;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var response  = await _accountService.Register(registerDto);
            return StatusCode(response.StatusCode, response);
        }
        else
        {
            var errorMessages = ModelState.SelectMany(e => e.Value.Errors.Select(e => e.ErrorMessage)).ToList();
            var response = new Response<RegisterDto>(HttpStatusCode.BadRequest, errorMessages);
            return StatusCode(response.StatusCode, response);
        }
        
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody]LoginDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var response  = await _accountService.Login(registerDto);
            return StatusCode(response.StatusCode, response);
        }
        else
        {
            var errorMessages = ModelState.SelectMany(e => e.Value.Errors.Select(e => e.ErrorMessage)).ToList();
            var response = new Response<RegisterDto>(HttpStatusCode.BadRequest, errorMessages);
            return StatusCode(response.StatusCode, response);
        }
        
    }


    
}
