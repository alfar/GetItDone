Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class ProjectModel
    Public Property Id As Integer
    Public Property Name As String
    <DisplayName("Created")> _
    Public Property CreatedDate As DateTime
End Class

Public Class ProjectReviewModel
    Inherits ProjectModel

    Public Property HasPurpose As Boolean
    Public Property HasPrinciples As Boolean
    Public Property HasVision As Boolean
    Public Property HasBrainstorm As Boolean
    Public Property HasOrganizing As Boolean
End Class

Public Class FutureProjectListModel
    Inherits ProjectModel
End Class

Public Class FinishedProjectListModel
    Inherits ProjectModel

    Public Property DoneDate As DateTime?
End Class

Public Class ProjectListModel
    Inherits ProjectModel

    Public Property NextActions As IEnumerable(Of TaskListModel)
End Class

Public Class ProjectDetailModel
    Inherits ProjectListModel

    <UIHint("MultilineText")>
    Public Property Purpose As String
    <UIHint("MultilineText")>
    Public Property Principles As String
    <UIHint("MultilineText")>
    Public Property Vision As String
    <UIHint("MultilineText")>
    Public Property Brainstorm As String
    <UIHint("MultilineText")>
    Public Property Organizing As String
End Class

Public Class CreateProjectModel
    <Required()> _
    Public Property Name As String

    <DisplayName("Create next action")>
    Public Property CreateNextAction As Boolean
    Public Property NextAction As String
    Public Property NextActionId As Integer?
End Class

Public Class ProjectService
    Private _model As ITaskModelContainer

    Public Sub New(model As ITaskModelContainer)
        _model = model
    End Sub

    Public Function GetProjectsForUser() As IQueryable(Of ProjectModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And Not p.Future And Not p.Finished Select New ProjectListModel With {.Id = p.Id, .Name = p.Name, .CreatedDate = p.CreatedDate, .NextActions = From t In p.Tasks Where Not t.Finished Select New TaskListModel With {.Id = t.Id, .Title = t.Title}}
    End Function

    Public Function GetFutureProjectsForUser() As IQueryable(Of ProjectModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Future And Not p.Finished Select New FutureProjectListModel With {.Id = p.Id, .Name = p.Name, .CreatedDate = p.CreatedDate}
    End Function

    Public Function GetFinishedProjectsForUser() As IQueryable(Of FinishedProjectListModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Finished Select New FinishedProjectListModel With {.Id = p.Id, .Name = p.Name, .CreatedDate = p.CreatedDate, .DoneDate = p.DoneDate}
    End Function

    Public Function GetEmptyProjectsForUser() As IQueryable(Of ProjectModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And Not p.Future And Not p.Tasks.Any() Select New ProjectModel With {.Id = p.Id, .Name = p.Name, .CreatedDate = p.createdDate}
    End Function

    Public Function CreateProject(name As String, Optional future As Boolean = False) As ProjectModel
        Dim member As MembershipUser = Membership.GetUser()

        Dim p As New Project With {.Name = name, .OwnerId = member.ProviderUserKey, .CreatedDate = DateTime.Now, .Future = future}

        _model.Projects.AddObject(p)

        _model.SaveChanges()

        Return New ProjectModel With {.Name = name, .Id = p.Id, .CreatedDate = p.CreatedDate}
    End Function

    Function GetProjectForUser(id As Integer) As ProjectDetailModel
        Dim member As MembershipUser = Membership.GetUser()
        Return (From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Id = id Select New ProjectDetailModel With {.Id = p.Id, .Name = p.Name, .Purpose = p.Purpose, .Principles = p.Principles, .Vision = p.Vision, .Brainstorm = p.Brainstorm, .Organizing = p.Organization, .CreatedDate = p.CreatedDate, .NextActions = From t In p.Tasks Where Not t.Finished Select New TaskListModel With {.Id = t.Id, .Title = t.Title, .ProjectName = t.Project.Name}}).FirstOrDefault()
    End Function

    Sub UpdateProject(id As Integer, data As ProjectDetailModel)
        Dim member As MembershipUser = Membership.GetUser()

        Dim proj As Project = (From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Id = id).FirstOrDefault
        If proj IsNot Nothing Then
            proj.Name = data.Name
            proj.Purpose = data.Purpose
            proj.Principles = data.Principles
            proj.Vision = data.Vision
            proj.Brainstorm = data.Brainstorm
            proj.Organization = data.Organizing

            _model.SaveChanges()
        End If
    End Sub

    Sub FinishProject(id As Integer)
        Dim member As MembershipUser = Membership.GetUser()

        Dim proj As Project = (From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Id = id).FirstOrDefault
        If proj IsNot Nothing Then
            proj.Finished = True
            proj.DoneDate = DateTime.Now()

            For Each t As Task In proj.Tasks
                If Not t.Finished Then
                    t.Finished = True
                    t.DoneDate = proj.DoneDate
                End If
            Next

            _model.SaveChanges()
        End If
    End Sub

    Sub DeleteProject(id As Integer, cleanTasks As Boolean)
        Dim member As MembershipUser = Membership.GetUser()

        Dim proj As Project = (From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Id = id).FirstOrDefault
        If proj IsNot Nothing Then
            For Each t As Task In proj.Tasks.ToList()
                If cleanTasks Then
                    _model.Tasks.DeleteObject(t)
                Else
                    t.Project = Nothing
                End If
            Next

            _model.Projects.DeleteObject(proj)
            _model.SaveChanges()
        End If
    End Sub

    Sub SetFuture(id As Integer, future As Boolean)
        Dim member As MembershipUser = Membership.GetUser()

        Dim proj As Project = (From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Id = id).FirstOrDefault
        If proj IsNot Nothing Then
            proj.Future = future
            _model.SaveChanges()
        End If
    End Sub

    Sub PromoteProject(id As Integer)
        SetFuture(id, False)
    End Sub

    Sub DemoteProject(id As Integer)
        SetFuture(id, True)
    End Sub
End Class
