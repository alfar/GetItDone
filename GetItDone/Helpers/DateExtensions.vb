Imports System.Runtime.CompilerServices

Public Module DateExtensions
    <Extension()> _
    Public Function TimeAgo(dt As DateTime) As HtmlString
        Dim diff As TimeSpan = DateTime.Now.Subtract(dt)
        Dim days As Integer = Math.Floor(diff.TotalDays)

        If days <= 0 Then
            If days = 0 Then
                Return New MvcHtmlString("Today")
            ElseIf days = -1 Then
                Return New MvcHtmlString("Tomorrow")
            Else
                Return New MvcHtmlString(String.Format("In {0} days", -days))
            End If
        Else
            If days < 1 Then
                Return New MvcHtmlString("Yesterday")
            Else
                Return New MvcHtmlString(String.Format("{0} days ago", days))
            End If
        End If
    End Function
End Module
