using OtoGaleri.Application.DTOs.UserDtos;

namespace OtoGaleri.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto?> LoginAsync(LoginDto loginDto);
}