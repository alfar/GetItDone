Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class ContextListModel
    Public Property Id As Integer
    Public Property Name As String
End Class

Public Class ContextModel
    Public Property Id As Integer
    <Required()>
    Public Property Name As String
End Class

Public Class ContextReviewModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property Tasks As IEnumerable(Of TaskListModel)
End Class

Public Class CreateContextModel
    <Required()>
    Public Property Name As String
End Class

Public Class ContextService
    Private _model As TaskModelContainer

    Public Sub New(model As TaskModelContainer)
        _model = model
    End Sub

    Public Function GetContextForUser(id As Integer) As ContextModel
        Dim member As MembershipUser = Membership.GetUser()

        Return (From c As Context In _model.Contexts Where c.OwnerId = member.ProviderUserKey AndAlso c.Id = id Select New ContextModel With {.Id = c.Id, .Name = c.Name}).FirstOrDefault()
    End Function

    Public Function GetContextsForUser() As IQueryable(Of ContextListModel)
        Dim member As MembershipUser = Membership.GetUser()

        Return From c As Context In _model.Contexts Where c.OwnerId = member.ProviderUserKey Select New ContextListModel With {.Id = c.Id, .Name = c.Name}
    End Function

    Public Function CreateContext(name As String) As ContextListModel
        Dim member As MembershipUser = Membership.GetUser()

        Dim c As New Context With {.Name = name, .OwnerId = member.ProviderUserKey}

        _model.Contexts.AddObject(c)
        _model.SaveChanges()

        Return New ContextListModel() With {.Id = c.Id, .Name = c.Name}
    End Function

    Public Function GetReviewContexts() As IQueryable(Of ContextReviewModel)
        Dim member As MembershipUser = Membership.GetUser()

        Return From c As Context In _model.Contexts Where c.OwnerId = member.ProviderUserKey And c.Tasks.Any() Select New ContextReviewModel With {.Id = c.Id, .Name = c.Name, .Tasks = (From t As Task In c.Tasks Select New TaskListModel() With {.Id = t.Id, .Title = t.Title})}
    End Function

    Public Sub UpdateContext(ctx As ContextModel)
        Dim member As MembershipUser = Membership.GetUser()

        Dim context As Context = (From c As Context In _model.Contexts Where c.OwnerId = member.ProviderUserKey AndAlso c.Id = ctx.Id).FirstOrDefault

        If context IsNot Nothing Then
            context.Name = ctx.Name
            _model.SaveChanges()
        End If
    End Sub

    Sub DeleteContext(id As Integer)
        Dim member As MembershipUser = Membership.GetUser()

        Dim context As Context = (From c As Context In _model.Contexts Where c.OwnerId = member.ProviderUserKey AndAlso c.Id = id).FirstOrDefault

        If context IsNot Nothing Then
            _model.Contexts.DeleteObject(context)
            _model.SaveChanges()
        End If
    End Sub

End Class
