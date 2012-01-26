Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class TaskModel
    Public Property Id As Integer
    Public Property Title As String
    Public Property Notes As String
End Class

Public Class TaskListModel
    Public Property Id As Integer
    Public Property Title As String
End Class

Public Class CreateTaskModel
    <Required()> _
    Public Property Title As String
End Class

Public Class AssignTaskModel
    Public Property Id As Integer
    Public Property Title As String
    Public Property Notes As String
    Public Property AssignToId As Integer
    Public Property AssignToName As String
End Class

Public Class ProcessTaskModel
    Public Property Id As Integer
    Public Property Title As String
    Public Property Contexts As IQueryable(Of ContextListModel)
End Class

Public Class DoTaskModel
    Public Property Tasks As IQueryable(Of TaskListModel)
    Public Property Contexts As IQueryable(Of ContextListModel)
End Class

Public Class TaskService
    Private _model As TaskModelContainer

    Public Sub New(model As TaskModelContainer)
        _model = model
    End Sub

    Public Function GetTasksForUser() As IQueryable(Of TaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.AssignedTo Is Nothing And t.Context IsNot Nothing Select New TaskListModel With {.Id = t.Id, .Title = t.Title}
    End Function

    Public Function GetTasksForContext(contextId As Integer) As IQueryable(Of TaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.AssignedTo Is Nothing And t.ContextId = contextId Select New TaskListModel With {.Id = t.Id, .Title = t.Title}
    End Function

    Public Function GetTasksForContexts(contextIds() As Integer) As IQueryable(Of TaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.AssignedTo Is Nothing And contextIds.Contains(t.ContextId) Select New TaskListModel With {.Id = t.Id, .Title = t.Title}
    End Function

    Public Sub CreateTask(title As String, ownerId As Guid)
        Dim ps As New PersonService(_model)
        Dim t As New Task() With {.Title = title, .Notes = "", .OwnerId = ownerId}

        _model.Tasks.AddObject(t)
        _model.SaveChanges()
    End Sub

    Public Sub DeleteTask(id As Integer)
        Dim ownerId As Guid = Membership.GetUser().ProviderUserKey
        _model.Tasks.DeleteObject(_model.Tasks.Where(Function(t) t.Id = id AndAlso t.OwnerId = ownerId).FirstOrDefault)
        _model.SaveChanges()
    End Sub

    Public Function GetNextTaskForProcessing() As ProcessTaskModel
        Dim member As MembershipUser = Membership.GetUser()

        Return (From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.Context Is Nothing And t.AssignedTo Is Nothing Select New ProcessTaskModel With {.Id = t.Id, .Title = t.Title, .Contexts = (From c As Context In _model.Contexts Where c.OwnerId = member.ProviderUserKey Select New ContextListModel With {.Id = c.Id, .Name = c.Name})}).FirstOrDefault()
    End Function

    Sub AssignContext(id As Integer, context As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.ContextId = context
            _model.SaveChanges()
        End If
    End Sub

    Sub AssignProject(id As Integer, project As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.ProjectId = project
            _model.SaveChanges()
        End If
    End Sub

    Function GetTaskForUser(id As Integer) As TaskModel
        Dim member As MembershipUser = Membership.GetUser()

        Return (From t In _model.Tasks Where t.Id = id AndAlso t.OwnerId = member.ProviderUserKey Select New TaskModel With {.Id = t.Id, .Title = t.Title, .Notes = t.Notes}).FirstOrDefault()
    End Function

    Sub AssignPerson(id As Integer, personId As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.AssignedToId = personId
            _model.SaveChanges()
        End If
    End Sub

End Class