using MakeYourHomework.AuthService.Models;
using MakeYourHomework.AuthService.Repositories.Abstraction;
using MakeYourHomework.AuthService.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace MakeYourHomework.AuthService.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IJwtTokenService _tokenService;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserRepository userRepository,
        IUserService userService,
        IJwtTokenService tokenService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _userService = userService;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<AuthResponse> SignInAsync(SigninModel signinModel)
    {
        AuthResponse authResponse;

        var userExists = await _userRepository.UserExistAsync(
                u => u.Nickname == signinModel.Login
                || u.Email == signinModel.Login);

        if (!userExists)
        {
            return new AuthResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = $"User {signinModel.Login} wasn't found."
            };
        }

        var user = await _userRepository.GetAsync(
            u => u.Nickname == signinModel.Login
            || u.Email == signinModel.Login);

        var result = await _signInManager.CheckPasswordSignInAsync(user, signinModel.Password, false);

        if (result.Succeeded)
        {
            var token = await _tokenService.CreateTokenAsync(user);
            return new AuthResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"User {signinModel.Login} has signed in successfully",
                JwToken = token
            };
        }
        else
        {
            return new AuthResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = $"Usen name/Emais or password is incorrect"
            };
        }
    }

    public async Task<AuthResponse> SignUpAsync(SignupModel signupModel)
    {
        if (await _userService.UserWithEmailExists(signupModel.Email))
        {
            return new AuthResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = $@"User with email ""{signupModel.Email}"" already exists"
            };
        }

        if (await _userService.UserWithNicknameExists(signupModel.Nickname))
        {
            return new AuthResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = $@"User with niclname ""{signupModel.Nickname}"" already exists"
            };
        }

        var user = new User
        {
            UserName = signupModel.Name,
            Email = signupModel.Email,
            Nickname = signupModel.Nickname,
            UserType = signupModel.UserType,
        };

        var result = await _userManager.CreateAsync(user, signupModel.Password);

        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"User hasn't been created. {result.Errors.FirstOrDefault()?.Description}"
            };
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, signupModel.Name),
            new Claim("Nickname", signupModel.Nickname),
            new Claim(ClaimTypes.Email, signupModel.Email),
            new Claim(ClaimTypes.Role, signupModel.UserType.ToString())
        };

        await _userManager.AddClaimsAsync(user, claims);
        await _userManager.AddToRoleAsync(user, signupModel.UserType.ToString());
        await _signInManager.SignInAsync(user, false);
        await _userRepository.SaveChangesAsync();

        var createdUser = await _userManager.FindByEmailAsync(user.Email);

        var token = await _tokenService.CreateTokenAsync(createdUser);

        return new AuthResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = $@"User with email ""{createdUser.Email}"" and nicname ""{createdUser.Nickname}"" has been created.",
            JwToken = token,
        };
    }
}
