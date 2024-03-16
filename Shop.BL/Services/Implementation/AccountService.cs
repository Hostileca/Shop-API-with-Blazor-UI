using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.BL.Dtos.User;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.BL.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountService(UserManager<User> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null && userRoles.Any())
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                signingCredentials: signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<UserReadDto> GetUser(string userName)
        {
            var identityUser = await _userManager.FindByNameAsync(userName);
            return _mapper.Map<UserReadDto>(identityUser);
        }

        public async Task<TokenDto> Login(UserRegisterDto userLoginDto)
        {
            var identityUser = await _userManager.FindByNameAsync(userLoginDto.UserName);
            if (identityUser is not null)
            {
                if (await _userManager.CheckPasswordAsync(identityUser, userLoginDto.Password))
                {
                    return new TokenDto()
                    {
                        Token = await GenerateToken(identityUser)
                    };
                }
            }
            throw new AuthenticationFailureException("Wrong username or password");
        }

        public async Task<TokenDto> RegisterUser(UserRegisterDto userRegisterDto)
        {
            if (await _userManager.FindByNameAsync(userRegisterDto.UserName) is not null)
            {
                throw new DuplicateNameException("Username is taken");
            }

            var user = new User
            {
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.UserName
            };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            var identityUser = await _userManager.FindByNameAsync(userRegisterDto.UserName);
            return new TokenDto()
            {
                Token = await GenerateToken(identityUser)
            };
        }

        public async Task<UserReadDto> UpdateUser(string userName, UserUpdateDto userUpdateDto)
        {
            var identityUser = await _userManager.FindByNameAsync(userName);
            await _userManager.ChangePasswordAsync(identityUser, userUpdateDto.CurrentPassword, userUpdateDto.NewPassword);
            identityUser.FirstName = userUpdateDto.FirstName;
            identityUser.LastName = userUpdateDto.LastName;
            await _userManager.UpdateAsync(identityUser);
            return _mapper.Map<UserReadDto>(identityUser);
        }
    }
}
