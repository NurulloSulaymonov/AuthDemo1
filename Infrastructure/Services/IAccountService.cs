using Domain.Dtos;
using Domain.Response;

namespace Infrastructure.Services;

public interface IAccountService
{
    Task<Response<RegisterDto>> Register(RegisterDto model);
}