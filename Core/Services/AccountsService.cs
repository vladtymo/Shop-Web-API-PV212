using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Core.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<User> userManager;

        public AccountsService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Register(RegisterDto model)
        {
            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                Birthdate = model.Birthdate,
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                //string all = string.Join(" ", result.Errors.Select(x => x.Description));
                var error = result.Errors.First();
                throw new HttpException(error.Description, HttpStatusCode.BadRequest);
            }
        }

        public async Task Login(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid login or password.", HttpStatusCode.BadRequest);

            // generate access token... (JWT)
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
