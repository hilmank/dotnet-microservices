namespace Student.Core.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Student.Core.Entities.Course>> GetAll();
    Task<Student.Core.Entities.Course> Get(string id);
    
}
