Public Interface ICourseRepository
    Function GetAll() As Task(Of IEnumerable(Of Course))
    Function GetById( id As String) As Task(Of Course)
End Interface
