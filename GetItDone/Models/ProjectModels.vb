Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class ProjectModel
    Public Property Id As Integer
    Public Property Name As String
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
End Class

Public Class ProjectService
    Private _model As TaskModelContainer

    Public Sub New(model As TaskModelContainer)
        _model = model
    End Sub

    Public Function GetProjectsForUser() As IQueryable(Of ProjectModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And Not p.Future Select New ProjectListModel With {.Id = p.Id, .Name = p.Name, .NextActions = From t In p.Tasks Select New TaskListModel With {.Id = t.Id, .Title = t.Title}}
    End Function

    Public Function GetFutureProjectsForUser() As IQueryable(Of ProjectModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Future Select New ProjectModel With {.Id = p.Id, .Name = p.Name}
    End Function

    Public Function GetEmptyProjectsForUser() As IQueryable(Of ProjectModel)
        Dim member As MembershipUser = Membership.GetUser()
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And Not p.Future And Not p.Tasks.Any() Select New ProjectModel With {.Id = p.Id, .Name = p.Name}
    End Function

    Public Function CreateProject(name As String) As ProjectModel
        Dim member As MembershipUser = Membership.GetUser()

        Dim p As New Project With {.Name = name, .OwnerId = member.ProviderUserKey}

        _model.Projects.AddObject(p)

        _model.SaveChanges()

        Return New ProjectModel With {.Name = name, .Id = p.Id}
    End Function

    Function GetProjectForUser(id As Integer) As ProjectDetailModel
        Dim member As MembershipUser = Membership.GetUser()
        Return (From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And p.Id = id Select New ProjectDetailModel With {.Id = p.Id, .Name = p.Name, .Purpose = p.Purpose, .Principles = p.Principles, .Vision = p.Vision, .Brainstorm = p.Brainstorm, .Organizing = p.Organization, .NextActions = From t In p.Tasks Select New TaskListModel With {.Id = t.Id, .Title = t.Title}}).FirstOrDefault()
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

End Class
