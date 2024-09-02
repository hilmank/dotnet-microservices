﻿namespace Student.Core.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student.Core.Entities.Student>> GetStudents();

}
