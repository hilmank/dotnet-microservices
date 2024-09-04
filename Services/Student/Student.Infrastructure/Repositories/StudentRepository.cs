using Dapper;
using Student.Core.Repositories;
using Student.Infrastructure.Extensions;

namespace Student.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    public async Task<Core.Entities.Student> GetStudent(string id)
    {
        await using var connection = await DapperConnectionProvider.ConnectionAsync();
        var result = await connection.GetListAsync<Core.Entities.Student>();
        return result?.Where(x => x.Id == id).FirstOrDefault()!;
    }

    public Task<IEnumerable<Core.Entities.Student>> GetStudents()
    {
        throw new NotImplementedException();
    }
}
