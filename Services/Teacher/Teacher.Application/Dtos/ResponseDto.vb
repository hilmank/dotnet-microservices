Public Class ResponseDto(Of T)
    Public Property Success As Boolean
    Public Property Message As String
    Public Property Data As T
End Class
