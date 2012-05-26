Imports Mvc.Mailer

Namespace GetItDone
    <Authorize()> _
    Public Class EmailController
        Inherits System.Web.Mvc.Controller

        Private service As New EmailService(New TaskModelContainer())

        '
        ' GET: /Email

        Function Index() As ActionResult
            Return View(service.GetEmailsForUser())
        End Function

        Function Confirm(id As String) As ActionResult
            service.ConfirmEmail(id)
            Return View()
        End Function

        '
        ' GET: /Email/Create

        Function Create() As ActionResult
            Return View()
        End Function

        '
        ' POST: /Email/Create

        <HttpPost()> _
        Function Create(input As CreateEmailModel) As ActionResult
            Try
                Dim em As ConfirmEmailModel = service.CreateEmailForUser(input.Email)
                If em IsNot Nothing Then
                    Dim mailer As New Mailers.EmailMailer
                    mailer.Confirm(em.Email, em.ConfirmationToken).Send()
                End If

                Return RedirectToAction("Index", "Profile")
            Catch
                Return View()
            End Try
        End Function


        Function Delete(ByVal id As Integer) As ActionResult
            Return View(service.GetEmailForUser(id))
        End Function

        '
        ' POST: /Email/Delete/5

        <HttpPost()> _
        Function Delete(ByVal id As Integer, fc As FormCollection) As ActionResult
            Try
                service.DeleteEmailForUser(id)

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function
    End Class
End Namespace
