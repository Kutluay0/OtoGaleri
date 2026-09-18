using OtoGaleri.Application.DTOs.UserDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Interfaces;
using OtoGaleri.Core.Entities;
using BCrypt.Net;

namespace OtoGaleri.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IGenericRepository<User> userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenResponseDto?> LoginAsync(LoginDto loginDto)
    {
        // 1. Kullanıcıyı e-posta adresine göre sorgula
        var users = await _userRepository.FindAsync(u => u.Email == loginDto.Email);
        var user = users.FirstOrDefault();

        if (user == null)
        {
            return null; // Kullanıcı bulunamadı
        }

        bool isPasswordValid = false;

        // 2. Hibrit Şifre Kontrolü:
        // Eğer veritabanındaki şifre '$' ile başlıyorsa BCrypt hash'idir, Verify ile doğrula.
        if (!string.IsNullOrEmpty(user.PasswordHash) && user.PasswordHash.StartsWith("$"))
        {
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            }
            catch
            {
                isPasswordValid = false;
            }
        }
        else
        {
            // Hash değilse (ilk eklenen düz metin şifreler), doğrudan düz metin olarak karşılaştır.
            isPasswordValid = (user.PasswordHash == loginDto.Password);
        }

        if (!isPasswordValid)
        {
            return null; // Şifre hatalı
        }

        // 3. Doğrulama başarılıysa Token oluştur ve dön
        return _tokenService.CreateToken(user);
    }
}