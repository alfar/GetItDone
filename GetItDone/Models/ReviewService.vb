Public Class ReviewInfo
    Public Property ActiveReview As ReviewModel
    Public Property LastReview As FinishedReviewModel
End Class

Public Class ReviewProjectInfo
    Public Property EmptyProjects As IQueryable(Of ProjectReviewModel)
    Public Property ActiveProjects As IQueryable(Of ProjectReviewModel)
    Public Property FinishedProjects As IQueryable(Of FinishedProjectListModel)
End Class

Public Class ReviewModel
    Public Property Started As DateTime
End Class

Public Class FinishedReviewModel
    Inherits ReviewModel
    Public Property Finished As DateTime
End Class

Public Class ReviewModel_old
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

    Public Function GetReviewDataForUser() As ReviewModel_old
        Dim member As MembershipUser = Membership.GetUser()

        Return New ReviewModel_old With {.Contexts = _contextService.GetReviewContexts(),
                                     .UnactionableProjects = _projectService.GetEmptyProjectsForUser(),
                                     .WaitingForPeople = From p In _model.People Where p.OwnerId = member.ProviderUserKey And p.UserId <> p.OwnerId Select New WaitingForReviewModel With {.Name = p.Name, .Tasks = (From t In p.Task Select New TaskListModel() With {.Id = t.Id, .Title = t.Title})}}
    End Function

    Function GetCalendarForUser() As IQueryable(Of CalendarTaskListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Dim lastweek As DateTime = DateTime.Now.AddDays(-7)
        Dim nextweek As DateTime = DateTime.Now.AddDays(7)

        Return From t In _model.Tasks Where t.OwnerId = member.ProviderUserKey And t.DueDate.HasValue And t.DueDate.Value > lastweek And t.DueDate.Value < nextweek Order By t.DueDate Select New CalendarTaskListModel With {.Id = t.Id, .Title = t.Title, .DueDate = t.DueDate, .Finished = t.Finished}
    End Function

    Function GetActiveReviewForUser() As ReviewModel
        Dim member As MembershipUser = Membership.GetUser()
        Return (From r In _model.Reviews Where r.OwnerId = member.ProviderUserKey And Not r.Finished.HasValue Select New ReviewModel() With {.Started = r.Started}).SingleOrDefault
    End Function

    Function GetLastReviewForUser() As FinishedReviewModel
        Dim member As MembershipUser = Membership.GetUser()
        Return (From r In _model.Reviews Where r.OwnerId = member.ProviderUserKey And r.Finished.HasValue Order By r.Finished Descending Select New FinishedReviewModel() With {.Started = r.Started, .Finished = r.Finished}).FirstOrDefault
    End Function

    Function GetReviewInfoForUser() As ReviewInfo
        Return New ReviewInfo() With {.ActiveReview = GetActiveReviewForUser(), .LastReview = GetLastReviewForUser()}
    End Function

    Function CreateReview() As ReviewModel
        Dim member As MembershipUser = Membership.GetUser()

        Dim r As New Review() With {.Id = Guid.NewGuid(), .Started = DateTime.Now, .OwnerId = member.ProviderUserKey}

        _model.Reviews.AddObject(r)
        _model.SaveChanges()

        Return New ReviewModel With {.Started = r.Started}
    End Function

    Function GetProjectInfo() As ReviewProjectInfo
        Return New ReviewProjectInfo With {.EmptyProjects = GetEmptyProjectsForUser(), .ActiveProjects = GetActiveProjectsForUser(), .FinishedProjects = GetCompletedProjects()}
    End Function

    Private Function GetEmptyProjectsForUser() As IQueryable(Of ProjectReviewModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And Not p.Future And Not p.Finished And Not p.Tasks.Any() Select New ProjectReviewModel With {.Id = p.Id, .Name = p.Name, .CreatedDate = p.CreatedDate, .HasBrainstorm = Not String.IsNullOrEmpty(p.Brainstorm), .HasVision = Not String.IsNullOrEmpty(p.Vision), .HasPurpose = Not String.IsNullOrEmpty(p.Purpose), .HasPrinciples = Not String.IsNullOrEmpty(p.Principles), .HasOrganizing = Not String.IsNullOrEmpty(p.Organization)}
    End Function

    Private Function GetActiveProjectsForUser() As IQueryable(Of ProjectReviewModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And Not p.Future And Not p.Finished And p.Tasks.Any() Select New ProjectReviewModel With {.Id = p.Id, .Name = p.Name, .CreatedDate = p.CreatedDate, .HasBrainstorm = Not String.IsNullOrEmpty(p.Brainstorm), .HasVision = Not String.IsNullOrEmpty(p.Vision), .HasPurpose = Not String.IsNullOrEmpty(p.Purpose), .HasPrinciples = Not String.IsNullOrEmpty(p.Principles), .HasOrganizing = Not String.IsNullOrEmpty(p.Organization)}
    End Function

    Private Function GetFutureProjectsForUser() As IQueryable(Of ProjectModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Future And Not p.Finished Select New FutureProjectListModel With {.Id = p.Id, .Name = p.Name, .CreatedDate = p.CreatedDate}
    End Function

    Function GetCompletedProjects() As IQueryable(Of FinishedProjectListModel)
        Dim rev As FinishedReviewModel = GetLastReviewForUser()
        If rev isnot Nothing then
            Return From t In _projectService.GetFinishedProjectsForUser() Where t.DoneDate.HasValue AndAlso t.DoneDate > rev.Finished
        Else
            Return _projectService.GetFinishedProjectsForUser()
        End If
    End Function

    Function GetFutureInfo() As IQueryable(Of FutureProjectListModel)
        Return GetFutureProjectsForUser()
    End Function

    Function GetDelegatedTasks() As IQueryable(Of AssigneeModel)
        Return _taskService.GetDelegatedTasksForUser()
    End Function

    Function GetCompletedTasks() As IQueryable(Of FinishedTaskListModel)
        Dim rev As FinishedReviewModel = GetLastReviewForUser()

        If rev IsNot Nothing Then
            Return From t In _taskService.GetFinishedTasksForUser() Where t.Finished > rev.Finished
        Else
            Return _taskService.GetFinishedTasksForUser()
        End If

    End Function

    Function GetProcessTask() As ProcessTaskModel
        Return _taskService.GetNextTaskForProcessing()
    End Function

    Sub FinishReview()
        Dim member As MembershipUser = Membership.GetUser()
        Dim rev As Review = (From r In _model.Reviews Where r.OwnerId = member.ProviderUserKey And Not r.Finished.HasValue).SingleOrDefault

        If rev IsNot Nothing Then
            rev.Finished = DateTime.Now
            _model.SaveChanges()
        End If
    End Sub

    Function GetNextActions() As IQueryable(Of ContextReviewModel)
        Dim member As MembershipUser = Membership.GetUser()

        Return From c In _model.Contexts Where c.OwnerId = member.ProviderUserKey And c.Tasks.Any(Function(t) Not t.Finished) Select New ContextReviewModel() With {.Id = c.Id, .Name = c.Name, .Tasks = c.Tasks.Where(Function(t) Not t.Finished).Select(Function(t) New TaskListModel With {.Id = t.Id, .ProjectName = t.Project.Name, .Title = t.Title})}

        '        From t In _model.Tasks Where Not t.Finished And Not t.DueDate.HasValue And Not t.AssignedToId.HasValue Select New TaskListModel() With {.Id = t.Id, .Title = t.Title, .ProjectName = t.Project.Name}()()
    End Function

End Class
