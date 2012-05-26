Imports System.Runtime.CompilerServices

Public Module MarkdownExtension
    <Extension()> _
    Public Function Markdown(html As HtmlHelper, text As String) As HtmlString
        Dim md As New MarkdownSharp.Markdown()
        Return New MvcHtmlString(md.Transform(text))
    End Function
End Module
