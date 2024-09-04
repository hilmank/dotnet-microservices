Imports System.Threading
Imports MediatR
Imports Teacher.Core

Public Class GetCoursesQueryHandler
    Implements IRequestHandler(Of GetCoursesQuery, IEnumerable(Of CourseDto))

    Private ReadOnly _courseRepository As ICourseRepository
    Public Sub New(courseRepository As ICourseRepository)
        _courseRepository = courseRepository
    End Sub

    Public Async Function Handle(request As GetCoursesQuery, cancellationToken As CancellationToken) As Task(Of IEnumerable(Of CourseDto)) Implements IRequestHandler(Of GetCoursesQuery,IEnumerable(Of CourseDto)).Handle
        Dim courses = Await _courseRepository.GetAll()
        Return courses.Select(Function(course) New CourseDto With{
                                 .Id = course.Id,
                                 .Name = course.Name,
                                 .Description = course.Description
                                 })
    End Function
End Class