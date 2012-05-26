Public Class EmailModel
    Public Property Id As Integer
    Public Property Email As String
    Public Property Confirmed As Boolean
End Class

Public Class CreateEmailModel
    Public Property Email As String
End Class

Public Class ConfirmEmailModel
    Public Property Id As Integer
    Public Property Email As String
    Public Property ConfirmationToken As String
End Class

Public Class EmailService
    Private _model As TaskModelContainer

    Public Sub New(model As TaskModelContainer)
        _model = model
    End Sub

    Public Function CreateConfirmationToken() As String
        Return Guid.NewGuid().ToString("N")
    End Function

    Function CreateEmailForUser(Email As String) As ConfirmEmailModel
        Dim user As MembershipUser = Membership.GetUser()
        Dim e As New UserEmail() With {.Email = Email, .OwnerId = user.ProviderUserKey, .ConfirmationToken = CreateConfirmationToken(), .Confirmed = False}
        _model.Emails.AddObject(e)
        _model.SaveChanges()
        Return New ConfirmEmailModel() With {.Id = e.Id, .Email = e.Email, .ConfirmationToken = e.ConfirmationToken}
    End Function

    Function GetEmailsForUser() As IQueryable(Of EmailModel)
        Dim user As MembershipUser = Membership.GetUser()
        Return From e In _model.Emails Where e.OwnerId = user.ProviderUserKey Select New EmailModel With {.Id = e.Id, .Email = e.Email, .Confirmed = e.Confirmed}
    End Function

    Sub DeleteEmailForUser(id As Integer)
        Dim user As MembershipUser = Membership.GetUser()
        Dim email As UserEmail = (From e In _model.Emails Where e.Id = id AndAlso e.OwnerId = user.ProviderUserKey).FirstOrDefault
        If email IsNot Nothing Then
            For Each p As Person In (From pe In _model.People Where pe.Email = email.Email)
                p.UserId = Nothing
            Next
            _model.Emails.DeleteObject(email)
            _model.SaveChanges()
        End If
    End Sub

    Sub ConfirmEmail(token As String)
        Dim user As MembershipUser = Membership.GetUser()
        Dim email As UserEmail = (From e In _model.Emails Where e.ConfirmationToken = token And e.OwnerId = user.ProviderUserKey).FirstOrDefault
        If email IsNot Nothing Then
            email.Confirmed = True
            _model.SaveChanges()
        End If
    End Sub

    Function GetEmailForUser(id As Integer) As EmailModel
        Dim user As MembershipUser = Membership.GetUser()
        Return (From e In _model.Emails Where e.Id = id AndAlso e.OwnerId = user.ProviderUserKey Select New EmailModel With {.Id = e.Id, .Email = e.Email, .Confirmed = e.Confirmed}).SingleOrDefault
    End Function
End Class