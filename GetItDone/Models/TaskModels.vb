Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class TaskModel
    Public Property Id As Integer
    Public Property Title As String
    <UIHint("MultilineText")>
    Public Property Notes As String
End Class

Public Class TaskListModel
    Public Property Id As Integer
    Public Property Title As String
    <UIHint("ProjectName")> _
    Public Property ProjectName As String
End Class

Public Class FinishedTaskListModel
    Inherits TaskListModel

    Public Property Finished As DateTime?
End Class

Public Class AssignedTaskModel
    Public Property Id As Integer
    Public Property Title As String
    Public Property OwnerId As Guid
    Public ReadOnly Property Owner As String
        Get
            Return Membership.GetUser(OwnerId).UserName
        End Get
    End Property
End Class

Public Class CreateTaskModel
    <Required()> _
    Public Property Title As String
End Class

Public Class FinishTaskModel
    Public Property Id As Integer
End Class

Public Class AssignTaskModel
    Public Property Id As Integer
    <Required()> _
    Public Property Title As String

    <UIHint("MultilineText")>
    Public Property Notes As String
    Public Property AssignType As Integer
    Public Property AssignToId As Integer
    Public Property AssignToName As String
    Public Property AssignToEmail As String

    Public Property SendType As Integer
End Class

Public Class CalendarTaskModel
    Public Property Id As Integer
    <Required()> _
    Public Property Title As String

    <UIHint("MultilineText")>
    Public Property Notes As String

    <Required()> _
    Public Property DueDate As DateTime
End Class

Public Class CalendarTaskListModel
    Public Property Id As Integer
    Public Property Title As String
    Public Property DueDate As DateTime
    Public Property Finished As Boolean
End Class

Public Class ProcessTaskModel
    Public Property Id As Integer
    Public Property Title As String
    Public Property Contexts As IQueryable(Of ContextListModel)
End Class

Public Class DoTaskModel
    Public Property Tasks As IQueryable(Of TaskListModel)
    Public Property Contexts As IQueryable(Of ContextListModel)
    Public Property CalendarTasks As IQueryable(Of TaskListModel)
    Public Property AgendaPeople As IQueryable(Of PersonListModel)
End Class

Public Class AgendaTaskModel
    Public Property Tasks As IQueryable(Of TaskListModel)
    Public Property Person As PersonModel
End Class

Public Class TaskService
    Private _model As TaskModelContainer

    Public Sub New(model As TaskModelContainer)
        _model = model
    End Sub

    Public Function GetTasksForUser(key As String) As IQueryable(Of TaskListModel)
        Dim guid As Guid

        If guid.TryParse(key, guid) Then
            Return GetTasksForUser(guid)
        End If

        Return Nothing
    End Function

    Public Function GetTasksForUser(Optional key As Guid = Nothing) As IQueryable(Of TaskListModel)
        If key = Guid.Empty Then
            Dim member As MembershipUser = Membership.GetUser()
            If member IsNot Nothing Then
                key = member.ProviderUserKey
            Else
                Return Nothing
            End If
        End If

        Return From t In _model.Tasks Where t.OwnerId = key And t.AssignedTo Is Nothing And t.Context IsNot Nothing And t.Context.Active And Not t.Finished Select New TaskListModel With {.Id = t.Id, .Title = t.Title, .ProjectName = t.Project.Name}
    End Function

    Public Function GetTodaysTasksForUser() As IQueryable(Of TaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Dim tomorrow As DateTime = DateTime.Today.AddDays(1)
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.AssignedTo Is Nothing And t.DueDate.HasValue And t.DueDate.Value < tomorrow And Not t.Finished Select New TaskListModel With {.Id = t.Id, .Title = t.Title, .ProjectName = t.Project.Name}
    End Function

    Public Function GetTasksForContext(contextId As Integer) As IQueryable(Of TaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.AssignedTo Is Nothing And t.ContextId = contextId And Not t.Finished Select New TaskListModel With {.Id = t.Id, .Title = t.Title, .ProjectName = t.Project.Name}
    End Function

    Public Function GetTasksForContexts(contextIds() As Integer) As IQueryable(Of TaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.AssignedTo Is Nothing And contextIds.Contains(t.ContextId) And Not t.Finished Select New TaskListModel With {.Id = t.Id, .Title = t.Title, .ProjectName = t.Project.Name}
    End Function

    Function GetAgendaTasksForUser(personId As Integer) As IQueryable(Of TaskListModel)
        Dim member As MembershipUser = Membership.GetUser()

        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And Not t.Finished And t.AgendaId = personId Select New TaskListModel With {.Id = t.Id, .Title = t.Title, .ProjectName = t.Project.Name}
    End Function

    Public Function CreateTask(title As String, notes As String, ownerId As Guid) As TaskListModel
        Dim ps As New PersonService(_model)
        Dim t As New Task() With {.Title = title, .Notes = notes, .OwnerId = ownerId, .CreatedDate = DateTime.Now}

        _model.Tasks.AddObject(t)
        _model.SaveChanges()

        Return New TaskListModel() With {.Id = t.Id, .Title = t.Title}
    End Function

    Public Sub DeleteTask(id As Integer)
        Dim ownerId As Guid = Membership.GetUser().ProviderUserKey
        _model.Tasks.DeleteObject(_model.Tasks.Where(Function(t) t.Id = id AndAlso t.OwnerId = ownerId).FirstOrDefault)
        _model.SaveChanges()
    End Sub

    Public Function GetNextTaskForProcessing() As ProcessTaskModel
        Dim member As MembershipUser = Membership.GetUser()

        Return (From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.Context Is Nothing And t.AssignedTo Is Nothing And t.AgendaTo Is Nothing And Not t.DueDate.HasValue And Not t.Finished Select New ProcessTaskModel With {.Id = t.Id, .Title = t.Title, .Contexts = (From c As Context In _model.Contexts Where c.OwnerId = member.ProviderUserKey Select New ContextListModel With {.Id = c.Id, .Name = c.Name})}).FirstOrDefault()
    End Function

    Function GetAssignedTasksForUser() As IQueryable(Of AssignedTaskModel)
        Dim member As MembershipUser = Membership.GetUser()

        Return From t In _model.Tasks Where t.AssignedTo.UserId = member.ProviderUserKey And Not t.Finished Select New AssignedTaskModel With {.Id = t.Id, .Title = t.Title, .OwnerId = t.OwnerId}
    End Function

    Sub AssignContext(id As Integer, context As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.ContextId = context
            _model.SaveChanges()
        End If
    End Sub

    Sub UnassignProject(id As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.ProjectId = Nothing
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

    Sub AssignPerson(id As Integer, personId As Integer, createInboxTask As Boolean)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.AssignedToId = personId

            If createInboxTask Then
                Dim p As Person = _model.People.FirstOrDefault(Function(p_) p_.Id = personId)
                Dim c As TaskListModel = CreateTask(task.Title, task.Notes, p.UserId)
            End If

            _model.SaveChanges()
        End If
    End Sub

    Function GetFinishingTask(id As Integer) As TaskModel
        Dim member As MembershipUser = Membership.GetUser()
        Return (From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.Id = id Select New TaskModel With {.Id = t.Id, .Title = t.Title, .Notes = t.Notes}).FirstOrDefault()
    End Function

    Sub FinishTask(id As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.DoneDate = DateTime.Now()
            task.Finished = True
            _model.SaveChanges()
        End If
    End Sub

    Sub PutInCalendar(calendar As CalendarTaskModel)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = calendar.Id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.DueDate = calendar.DueDate
            _model.SaveChanges()
        End If
    End Sub

    Function GetFinishedTasksForUser() As IQueryable(Of FinishedTaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.Finished Order By t.DoneDate Descending Select New FinishedTaskListModel With {.Id = t.Id, .Title = t.Title, .Finished = t.DoneDate}
    End Function

    Sub UpdateTask(id As Integer, Title As String, Notes As String)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.Title = Title
            If Notes IsNot Nothing Then
                task.Notes = Notes
            End If
            _model.SaveChanges()
        End If
    End Sub

    Sub Reprocess(id As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.AssignedToId = Nothing
            task.AgendaId = Nothing
            task.DueDate = Nothing
            task.ContextId = Nothing
            _model.SaveChanges()
        End If
    End Sub

    Function GetDelegatedTasksForUser() As IQueryable(Of AssigneeModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.AssignedTo IsNot Nothing And Not t.Finished Group By Assignee = t.AssignedTo Into Tasks = Group Select New AssigneeModel With {.Id = Assignee.Id, .Name = Assignee.Name, .Tasks = From task In Tasks Select New TaskListModel With {.Id = task.Id, .Title = task.Title}}
    End Function

    Sub AssignAgenda(id As Integer, personId As Integer)
        Dim member As MembershipUser = Membership.GetUser()
        Dim task As Task = _model.Tasks.FirstOrDefault(Function(t) t.Id = id AndAlso t.OwnerId = member.ProviderUserKey)

        If task IsNot Nothing Then
            task.AgendaId = personId
            _model.SaveChanges()
        End If
    End Sub
End Class