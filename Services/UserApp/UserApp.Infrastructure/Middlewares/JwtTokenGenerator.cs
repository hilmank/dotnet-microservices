using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserApp.Application.Middlewares;
using UserApp.Application.Settings;
using UserApp.Core.Entities;

namespace UserApp.Infrastructure.Middlewares;

public class JwtTokenGenerator : IJwtTokenGenerator
{
            private static int RandomNumber(int min, int max)
        {
            Random random = new();
            return random.Next(min, max);
        }
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new();
            Random random = new();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtSettings.AppSecret)),
                SecurityAlgorithms.HmacSha256);

        var claims = new[]
        { new Claim("userid", user.Id.ToString()) };

        var securityToken = new JwtSecurityToken(
            issuer: JwtSettings.AppIssuer,
            audience: JwtSettings.AppAudience,
            expires: DateTime.Now.AddMinutes(JwtSettings.AppExpireMinutes),
            claims: claims,
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public string GeneratePassword()
    {
        StringBuilder builder = new();
        builder.Append(RandomString(4, true));
        builder.Append(RandomNumber(1000, 9999));
        builder.Append(RandomString(2, false));
        return builder.ToString();
    }
}
