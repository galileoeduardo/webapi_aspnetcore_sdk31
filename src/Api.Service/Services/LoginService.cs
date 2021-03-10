using Api.Domain.Entities;
using Api.Domain.Repository;
using Domain.Dtos;
using Domain.Interfaces.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;

        public LoginService(IUserRepository repository)
        {
            _repository = repository;

        }

        public async Task<object> FindByLogin(LoginDto User)
        {
            if (!User.Equals(null) && !string.IsNullOrWhiteSpace(User.Email))
            {
                return await _repository.findByLogin(User.Email);
            }

            return null;
            
        }
    }
}
