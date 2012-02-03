Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class ProjectModel
    Public Property Id As Integer
    Public Property Name As String
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
        Return From p In _model.Projects Where p.OwnerId = member.ProviderUserKey And Not p.Future Select New ProjectModel With {.Id = p.Id, .Name = p.Name}
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
End Class
