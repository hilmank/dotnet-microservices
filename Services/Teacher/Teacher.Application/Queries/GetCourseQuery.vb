Imports MediatR

Public Class GetCourseQuery
    Implements IRequest(Of ResponseDto(Of CourseDto))
    Public Property Id As String
End Class