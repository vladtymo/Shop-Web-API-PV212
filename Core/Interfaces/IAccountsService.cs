using Core.Dtos;

namespace Core.Interfaces
{
    public interface IAccountsService
    {
        Task Register(RegisterDto model);
        Task Login(LoginDto model);
        Task Logout();
    }
}
