Public Class ReviewModel
    Public Property Contexts As IQueryable(Of ContextReviewModel)
    Public Property UnactionableProjects As IQueryable(Of ProjectModel)
    Public Property WaitingForPeople As IQueryable(Of WaitingForReviewModel)
End Class

Public Class WaitingForReviewModel
    Public Property Name As String
    Public Property Tasks As IEnumerable(Of TaskListModel)
End Class

Public Class ReviewService
    Private _model As TaskModelContainer
    Private _taskService As TaskService
    Private _contextService As ContextService
    Private _projectService As ProjectService
    Private _personService As PersonService

    Public Sub New(model As TaskModelContainer)
        _model = model
        _taskService = New TaskService(model)
        _contextService = New ContextService(model)
        _projectService = New ProjectService(model)
        _personService = New PersonService(model)
    End Sub

    Public Function GetReviewDataForUser() As ReviewModel
        Dim member As MembershipUser = Membership.GetUser()

        Return New ReviewModel With {.Contexts = _contextService.GetReviewContexts(),
                                     .UnactionableProjects = _projectService.GetEmptyProjectsForUser(),
                                     .WaitingForPeople = From p In _model.People Where p.OwnerId = member.ProviderUserKey And p.UserId <> p.OwnerId Select New WaitingForReviewModel With {.Name = p.Name, .Tasks = (From t In p.Task Select New TaskListModel() With {.Id = t.Id, .Title = t.Title})}}
    End Function
End Class
