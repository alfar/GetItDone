

Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports Mvc.Mailer
Imports System.Net.Mail

Namespace GetItDone.Mailers

    Public Interface ITaskMailer

        Function Assign(t As AssignTaskModel) As MailMessage

    End Interface

End Namespace
