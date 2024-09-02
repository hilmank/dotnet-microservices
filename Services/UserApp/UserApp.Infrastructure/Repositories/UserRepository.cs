using Dapper;
using Grpc.Core;
using UserApp.Core.Entities;
using UserApp.Core.Repositories;
using UserApp.Infrastructure.Extensions;

namespace UserApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<IEnumerable<User>> GetAll()
    {
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        return await connection.GetListAsync<User>();
    }
    public async Task<User> Get(string id)
    {
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        return await connection.GetAsync<User>(id);
    }
    public async Task<User> GetByUsernameOrEmail(string usernameOrEmail)
    {
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        var results = await connection.GetListAsync<User>("WHERE Username = @UsernameOrEmail OR Email = @UsernameOrEmail",
                                                      new { UsernameOrEmail = usernameOrEmail });

        return results?.FirstOrDefault()!;
    }
    public async Task<bool> Create(User user, Action<User> result)
    {
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        connection.Open();
        using var tx = connection.BeginTransaction();
        _ = connection.Insert<string, User>(user);
        if (user.Files is not null)
            connection.Execute(UserFile.SqlInsert, user.Files);
        if (user.Roles is not null)
            connection.Execute(UserRole.SqlInsert, user.Roles.Select(role => new { UserId = user.Id, RoleId = role.Id }));
        result(user);
        tx.Commit();
        return true;
    }
    public async Task<bool> Update(User user, Action<User> result)
    {
        string methodName = "Update";
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        connection.Open();
        using var tx = connection.BeginTransaction();
        var itemExist = connection.Get<User>(user.Id);

        //save hitory
        var userHistory = itemExist.SetHistory(user.UpdatedBy!, $"{GetType().Name}.{methodName}");
        _ = connection.ExecuteScalar(userHistory.SqlSave(), userHistory);
        if (user.Files is not null)
        {
            var userFiles = connection.GetList<UserFile>()
            .Where(x => user.Files.Any(pair => pair.Id == x.Id & pair.Type == x.Type))
            .Select(x =>
            {
                var hitory = x.SetHistory(user.UpdatedBy!, $"{GetType().Name}.{methodName}");
                connection.ExecuteScalar(hitory.SqlSave(), hitory);
                return x;
            }); ;
            connection.DeleteList<UserFile>("Id = @Id AND Type IN @Types", new { Id = user.Id, Types = user.Files.Select(x => x.Type).ToList() });
            connection.Execute(UserFile.SqlInsert, user.Files);
        }
        if (user.Roles is not null)
        {
            var userRoleExist = connection.GetList<UserRole>()
                                            .Where(x => x.Id == user.Id)
                                            .Select(x =>
                                            {
                                                var hitory = x.SetHistory(user.UpdatedBy!, $"{GetType().Name}.{methodName}");
                                                connection.ExecuteScalar(hitory.SqlSave(), hitory);
                                                return x;
                                            });

            _ = connection.DeleteList<UserRole>(new { Id = user.Id });

            connection.Execute(UserRole.SqlInsert, user.Roles.Select(role => new { UserId = user.Id, RoleId = role.Id }));
        }
        _ = connection.Update(user);
        tx.Commit();
        result(user);
        return true;
    }
    public async Task<bool> Delete(string userId, string id, Action<string> result)
    {
        string methodName = "Delete";
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        connection.Open();
        using var tx = connection.BeginTransaction();
        var user = connection.Get<User>(id);

        //save hitory
        var userHistory = user.SetHistory(userId, $"{GetType().Name}.{methodName}");
        _ = connection.ExecuteScalar(userHistory.SqlSave(), userHistory);

        var userFiles = connection.GetList<UserFile>()
        .Where(x => x.Id == id)
        .Select(x =>
        {
            var hitory = x.SetHistory(userId, $"{GetType().Name}.{methodName}");
            connection.ExecuteScalar(hitory.SqlSave(), hitory);
            return x;
        }); ;
        var userRoleExist = connection.GetList<UserRole>()
                                        .Where(x => x.Id == user.Id)
                                        .Select(x =>
                                        {
                                            var hitory = x.SetHistory(userId, $"{GetType().Name}.{methodName}");
                                            connection.ExecuteScalar(hitory.SqlSave(), hitory);
                                            return x;
                                        });

        _ = connection.DeleteList<UserFile>(new { Id = id });
        _ = connection.DeleteList<UserRole>(new { Id = id });
        _ = connection.DeleteList<User>(new { Id = id });
        tx.Commit();
        result("success");
        return true;
    }
}
