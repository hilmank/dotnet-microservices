using System;
using UserApp.Core.Entities;

namespace UserApp.Application.Middlewares;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);

}
