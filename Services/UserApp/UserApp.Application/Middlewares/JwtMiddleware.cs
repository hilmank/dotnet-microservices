using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserApp.Application.Settings;
using UserApp.Core.Entities;
using UserApp.Core.Repositories;

namespace UserApp.Application.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository user)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, user, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserRepository user, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(JwtSettings.AppSecret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = JwtSettings.AppAudience,
                    ValidIssuer = JwtSettings.AppIssuer,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "userid").Value;

                // attach user to context on successful jwt validation
                var result = user.Get(userId).Result;
                context.Items["User"] = result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on Validate Token: {ex.Message}");
            }

        }
    }
}