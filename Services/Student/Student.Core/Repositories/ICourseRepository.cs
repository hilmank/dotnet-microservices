namespace Student.Core.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Student.Core.Entities.Course>> GetCourses();
}
