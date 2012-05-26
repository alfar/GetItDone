

Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports Mvc.Mailer
Imports System.Net.Mail

Namespace GetItDone.Mailers

    Public Interface IEmailMailer

        Function Confirm(email As String, token As String) As MailMessage

    End Interface

End Namespace
