using Shop.BL.Dtos.User;
using Shop.DAL.Models;

namespace Shop.BL.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<string> GenerateToken(User user);
        public Task<TokenDto> RegisterUser(UserRegisterDto userRegisterDto);
        public Task<TokenDto> Login(UserRegisterDto userLoginDto);
        public Task<UserReadDto> UpdateUser(string userName, UserUpdateDto userUpdateDto);
        public Task<UserReadDto> GetUser(string userName);
    }
}
