Imports System.Threading
Imports MediatR
Imports Teacher.Core

Public Class GetCourseQueryHandler
    Implements IRequestHandler(Of GetCourseQuery, ResponseDto(Of CourseDto))
    Private ReadOnly _courseRepository As ICourseRepository

    Public Sub New(courseRepository As ICourseRepository)
        _courseRepository = courseRepository
    End Sub

    Public Async Function Handle(request As GetCourseQuery, cancellationToken As CancellationToken) As Task(Of ResponseDto(Of CourseDto)) Implements IRequestHandler(Of GetCourseQuery,ResponseDto(Of CourseDto)).Handle
        Dim course = Await _courseRepository.GetById(request.Id)
        Return New ResponseDto(Of CourseDto) With {
            .Success = course IsNot Nothing,
            .Message = If(course IsNot Nothing, "", "No data found"),
            .Data = New CourseDto With{
                .Id = course.Id,
                .Name = course.Name,
                .Description = course.Description
                }
            }
    End Function
End Class