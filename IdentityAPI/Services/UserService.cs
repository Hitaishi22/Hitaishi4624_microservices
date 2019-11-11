namespace Identity.WebApi.Services
{
 
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Models;
    public interface IUserService
    {
        Models.SecurityToken Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { AccountNumber = 3628101, Currency = "EUR", FullName = "Simon Peter", Username = "hitaishi", Password = "test@123" },
            new User { AccountNumber = 3637897, Currency = "EUR", FullName = "Glen Woodhouse", Username = "anurag", Password = "pass@123" },
            new User { AccountNumber = 3648755, Currency = "EUR", FullName = "John Smith", Username = "divanshu", Password = "admin@123" },
        };


        public UserService()
        {
        }

        public Models.SecurityToken Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("accountnumber", user.AccountNumber.ToString()),
                    new Claim("currency", user.Currency),
                    new Claim("name", user.FullName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new Models.SecurityToken() { auth_token = jwtSecurityToken };
        }
    }
}
