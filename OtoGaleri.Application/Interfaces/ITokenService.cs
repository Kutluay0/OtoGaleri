using OtoGaleri.Application.DTOs.UserDtos;
using OtoGaleri.Core.Entities;

namespace OtoGaleri.Application.Interfaces;

public interface ITokenService
{
    TokenResponseDto CreateToken(User user);
}