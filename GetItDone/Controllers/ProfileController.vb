Namespace GetItDone
    <Authorize()>
    Public Class ProfileController
        Inherits GetItDone.GetToDoneControllerBase

        Private service As New PersonService(container)

        '
        ' GET: /Person

        Function Index() As ActionResult
            If User.Identity Is Nothing Then
                ' No user found, redirect to home
                Return RedirectToAction("Index", "Home")
            End If

            Return View(service.GetProfileForUser())
        End Function
    End Class
End Namespace
