Public Class ContextListModel
    Public Property Id As Integer
    Public Property Name As String
End Class

Public Class ContextReviewModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property Tasks As IEnumerable(Of TaskListModel)
End Class

Public Class CreateContextModel
    Public Property Name As String
End Class

Public Class ContextService
    Private _model As TaskModelContainer

    Public Sub New(model As TaskModelContainer)
        _model = model
    End Sub

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

End Class
