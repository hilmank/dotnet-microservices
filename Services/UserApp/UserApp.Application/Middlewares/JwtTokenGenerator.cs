using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserApp.Application.Settings;
using UserApp.Core.Entities;

namespace UserApp.Application.Middlewares;

public class JwtTokenGenerator : IJwtTokenGenerator
{
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
}
