using AutoMapper;
using OtoGaleri.Application.DTOs.UserDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Entities;
using OtoGaleri.Core.Interfaces;

namespace OtoGaleri.Application.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IGenericRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task AddUserAsync(CreateUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        // PasswordHash İşlemi: Kullanıcının girdiği açık şifreyi BCrypt ile güvenli hash'e dönüştürüyoruz
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UpdateUserDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(userDto.Id);
        if (user != null)
        {
            _mapper.Map(userDto, user);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            user.IsDeleted = true;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}