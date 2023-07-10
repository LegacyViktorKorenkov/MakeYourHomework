using AutoMapper;
using MakeYourHomework.AuthService.Dtos;
using MakeYourHomework.AuthService.Models;
using MakeYourHomework.AuthService.Repositories.Abstraction;
using MakeYourHomework.AuthService.Services.Abstraction;
using System.Linq.Expressions;
using System.Net.Mail;

namespace MakeYourHomework.AuthService.Services;

public class UserService : IUserService
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        ILogger<UserService> logger,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(Expression<Func<User, bool>> filter = null)
    {
        var users = await _userRepository.GetAllAsync();

        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

        _logger.LogInformation($"Users were recieved. Number of users: {userDtos.Count()}");

        return userDtos;
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetAsync(u => u.Id == id);

        var userDto = _mapper.Map<UserDto>(user);

        _logger.LogInformation($"User with ID {userDto.Id} was recieved");

        return userDto;
    }

    public async Task<UserDto> GetUserByNicknameOrEmailAsync(string param)
    {
        var user
            = await _userRepository.GetAsync(u => u.Nickname == param)
            ?? await _userRepository.GetAsync(u => u.Email == param);

        var userDto = _mapper.Map<UserDto>(user);

        _logger.LogInformation($"User with ID {userDto.Id} was recieved");

        return userDto;
    }

    public Task<bool> UserWithEmailExists(string email)
    {
        MailAddress mailAddress;

        try
        {
            mailAddress = new MailAddress(email);
        }
        catch
        {
            throw new Exception("Email is incorrect");
        }

        return _userRepository.UserExistAsync(u => u.Email == mailAddress.Address);
    }

    public Task<bool> UserWithNicknameExists(string nickname) 
        => _userRepository.UserExistAsync(u => u.Nickname == nickname);
}
