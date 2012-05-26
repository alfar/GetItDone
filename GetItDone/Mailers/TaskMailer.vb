Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports Mvc.Mailer
Imports System.Net.Mail

Namespace GetItDone.Mailers

    Public Class TaskMailer
        Inherits MailerBase

        Implements ITaskMailer


        Public Sub New(model As TaskModelContainer)
            MyBase.New()
            MasterName = "_Layout"
            _model = model
            personService = New PersonService(_model)
        End Sub

        Private _model As TaskModelContainer
        Private personService As PersonService

        Public Overridable Function Assign(t As AssignTaskModel) As MailMessage Implements ITaskMailer.Assign

            Dim mailMessage = New MailMessage() With {
                .Subject = "GetToDone: " & t.Title
            }

            mailMessage.To.Add(t.AssignToEmail)
            ViewBag.Sender = personService.GetProfileForUser()
            mailMessage.ReplyToList.Add(ViewBag.sender.Email)
            ViewBag.Data = t
            PopulateBody(mailMessage, viewName:="Assign")

            Return mailMessage
        End Function


    End Class

End Namespace
