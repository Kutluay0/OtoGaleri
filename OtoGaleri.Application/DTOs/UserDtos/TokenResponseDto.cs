namespace OtoGaleri.Application.DTOs.UserDtos;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}