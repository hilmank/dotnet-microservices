using Student.Core.Entities;

namespace Student.Core.Repositories;

public interface IRegistrationRepository
{
    Task<IEnumerable<Student.Core.Entities.Enrollment>> GetStudentRegistrations();
    Task<Student.Core.Entities.Enrollment> GetStudentRegistration(string id);
    Task<bool> CreateRegistration(Student.Core.Entities.Enrollment enrollment);
    Task<bool> UpdateRegistration(Student.Core.Entities.Enrollment enrollment);
    Task<bool> DeleteRegistration(string id);
}
