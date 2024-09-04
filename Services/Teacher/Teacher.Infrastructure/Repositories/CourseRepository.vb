Imports Teacher.Core.Entities
Imports Teacher.Core.Repositories
Imports Teacher.Infrastructure.Extensions
Imports Dapper
Imports Teacher.Core

Public Class CourseRepository
    Implements ICourseRepository

    Public Async Function GetAll() As Task(Of IEnumerable(Of Course)) Implements ICourseRepository.GetAll
        Using connection = Await DapperConnectionProvider.ConnectionAsync()
            Dim courses = Await connection.GetListAsync (Of Course)()
            Return courses
        End Using
    End Function

    Public Async Function GetById(id As String) As Task(Of Course) Implements ICourseRepository.GetById
        Using connection = Await DapperConnectionProvider.ConnectionAsync()
            Return Await connection.GetAsync (Of Course)(id)
        End Using
    End Function
End Class