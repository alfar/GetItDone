

Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports Mvc.Mailer
Imports System.Net.Mail

Namespace GetItDone.Mailers

    Public Class EmailMailer
        Inherits MailerBase

        Implements IEmailMailer


        Public Sub New()
            MyBase.New()
            MasterName = "_Layout"
        End Sub


        Public Overridable Function Confirm(email As String, token As String) As MailMessage Implements IEmailMailer.Confirm
            Dim user As MembershipUser = Membership.GetUser()

            Dim mailMessage = New MailMessage() With {
                .Subject = "Confirm"
            }

            mailMessage.To.Add(email)

            ViewBag.Email = email
            ViewBag.Token = token
            ViewBag.UserName = user.UserName()
            PopulateBody(mailMessage, viewName:="Confirm")

            Return mailMessage
        End Function


    End Class

End Namespace
