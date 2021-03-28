using Api.Domain.Entities;
using Api.Domain.Repository;
using Domain.Dtos;
using Domain.Interfaces.Services.User;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _Repository;
        private readonly SignConfigurations _SignConfigurations;
        private readonly TokenConfigurations _TokenConfigurations;
        private IConfiguration _Configuration { get; }

        public LoginService(IUserRepository repository,
            SignConfigurations SignConfigurations,
            TokenConfigurations TokenConfigurations,
            IConfiguration Configuration)
        {
            _Repository = repository;
            _SignConfigurations = SignConfigurations;
            _TokenConfigurations = TokenConfigurations;
            _Configuration = Configuration;
        }

        public async Task<object> FindByLogin(LoginDto User)
        {
            
            if (User != null && !string.IsNullOrWhiteSpace(User.Email))
            {
                UserEntity baseUser = await _Repository.findByLogin(User.Email);

                if (baseUser == null)       
                {
                    return new {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    ClaimsIdentity identify = new ClaimsIdentity(
                        new GenericIdentity(User.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Email)
                        }

                    );

                    DateTime createDate = DateTime.Now;
                    DateTime exprirationDate = createDate + TimeSpan.FromSeconds(_TokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identify,createDate,exprirationDate,handler);

                    return SucessObject(createDate, exprirationDate, token, User);
                }
            }

            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };

        }

        

        private string CreateToken(ClaimsIdentity identy, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securutyToken = handler.CreateToken(new SecurityTokenDescriptor {
                Issuer = _TokenConfigurations.Issuer,
                Audience = _TokenConfigurations.Audience,
                SigningCredentials = _SignConfigurations.SigningCredentials,
                Subject = identy,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securutyToken);
            return token;

        }

        private object SucessObject(DateTime createDate, DateTime exprirationDate, string token, LoginDto user)
        {
            return new
            {
                Authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = exprirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário Logado com sucesso"

            };
        }
    }
}
