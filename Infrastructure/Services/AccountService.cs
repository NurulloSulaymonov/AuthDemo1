using System.Net;
using Domain.Dtos;
using Domain.Response;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Response<RegisterDto>> Register(RegisterDto model)
    {
        var mapped = new IdentityUser()
        {
            UserName = model.Username,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber
        };

        var response = await _userManager.CreateAsync(mapped,model.Password);
        if (response.Succeeded == true)
            return new Response<RegisterDto>(model);
        else return new Response<RegisterDto>(HttpStatusCode.BadRequest, "something is wrong");

    }
    
    
}