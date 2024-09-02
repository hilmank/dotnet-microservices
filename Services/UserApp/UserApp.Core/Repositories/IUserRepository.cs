using UserApp.Core.Entities;

namespace UserApp.Core.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> Get(string id);
    Task<User> GetByUsernameOrEmail(string usernameOrEmail);
    Task<bool> Create(User user, Action<User> result);
    Task<bool> Update(User user, Action<User> result);
    Task<bool> Delete(string userId, string id, Action<string> result);
}
