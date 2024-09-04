using Student.Core.Entities;
using Student.Core.Repositories;
using Student.Infrastructure.Extensions;
using Dapper;

namespace Student.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    public async Task<IEnumerable<Course>> GetAll()
    {
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        var course = await connection.GetListAsync<Course>();
        return await connection.GetListAsync<Course>();
    }
    public async Task<Course> Get(string id)
    {
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        return connection.Get<Course>(id);
    }
}
