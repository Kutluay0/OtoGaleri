using OtoGaleri.Application.DTOs.UserDtos;

namespace OtoGaleri.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task AddUserAsync(CreateUserDto userDto);
    Task UpdateUserAsync(UpdateUserDto userDto);
    Task DeleteUserAsync(int id);
}