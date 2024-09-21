using Core.Dtos;

namespace Core.Interfaces
{
    public interface IAccountsService
    {
        Task Register(RegisterDto model);
        Task<UserTokens> Login(LoginDto model);
        Task Logout(string refreshToken);
        Task<UserTokens> RefreshTokens(UserTokens tokens);
        Task RemoveExpiredRefreshTokens();
    }
}
