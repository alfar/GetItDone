Imports System.Runtime.CompilerServices

Public Module MarkdownExtension

    Private NextNextActionRegEx As New System.Text.RegularExpressions.Regex("%(.*)%", RegexOptions.Multiline)

    <Extension()> _
    Public Function Markdown(html As HtmlHelper, text As String, Optional projectId As Integer = 0) As HtmlString
        Dim md As New MarkdownSharp.Markdown()
        Return New MvcHtmlString(NextNextActionRegEx.Replace(md.Transform(text), Function(m As Match) String.Format("<a href=""/task/collect?title={0}&projectid={1}"">{0}</a>", m.Groups(1).Value, projectId)))
    End Function
End Module
