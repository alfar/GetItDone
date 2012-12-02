Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class ProfileModel
    <Required()> _
    <Display(Name:="Full name")> _
    Public Property Name As String

    <Required()> _
    <DataType(DataType.EmailAddress)> _
    <Display(Name:="Email address")> _
    Public Property Email As String

    Public Property Emails As IQueryable(Of EmailModel)

    Public Property APIKey As Guid
End Class

Public Class AssigneeModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property Tasks As IEnumerable(Of TaskListModel)
End Class

Public Class PersonModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property Email As String
    Public Property HasAccount As Boolean
End Class

Public Class PersonDetailModel
    Inherits PersonModel

    Public Property AssignedTasks As IQueryable(Of TaskListModel)
    Public Property AgendaTasks As IQueryable(Of TaskListModel)
End Class

Public Class PersonListModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property DelegatedTasks As Integer
    Public Property AgendaTasks As Integer
End Class

Public Class CreatePersonModel
    <Required()> _
    Public Property Name As String
    Public Property Email As String
End Class

Public Class PersonService
    Private _model As ITaskModelContainer

    Public Sub New(model As ITaskModelContainer)
        _model = model
    End Sub

    Public Function GetPersonForUser() As Person
        Dim user As MembershipUser = Membership.GetUser()
        Dim person As Person = _model.People.Where(Function(p) p.UserId = user.ProviderUserKey).FirstOrDefault

        If person Is Nothing Then
            person = New Person()
            person.UserId = user.ProviderUserKey
            person.OwnerId = user.ProviderUserKey
            person.Email = user.Email
            person.Name = user.UserName

            Dim es As New EmailService(_model)
            es.CreateEmailForUser(person.Email)

            _model.People.AddObject(person)

            _model.SaveChanges()
        End If

        Dim persons As IQueryable(Of Person) = From p In _model.People Join e In _model.Emails On p.Email Equals e.Email Where p.UserId <> user.ProviderUserKey And e.OwnerId = user.ProviderUserKey Select p

        If persons.Any Then
            For Each p As Person In persons
                p.UserId = user.ProviderUserKey
            Next
            _model.SaveChanges()
        End If

        Return person
    End Function

    Public Function GetPersonForUser(id As Integer) As PersonModel
        Dim user As MembershipUser = Membership.GetUser()
        Return (From p In _model.People Where p.OwnerId = user.ProviderUserKey And p.Id = id Select New PersonModel() With {.Id = p.Id, .Name = p.Name, .Email = p.Email, .HasAccount = p.UserId <> Guid.Empty}).FirstOrDefault
    End Function

    Public Function GetPersonDetailsForUser(id As Integer) As PersonDetailModel
        Dim user As MembershipUser = Membership.GetUser()
        Return (From p In _model.People Where p.OwnerId = user.ProviderUserKey And p.Id = id Select New PersonDetailModel() With {.Id = p.Id, .Name = p.Name, .Email = p.Email, .HasAccount = p.UserId <> Guid.Empty, .AgendaTasks = (From t As Task In _model.Tasks Where t.OwnerId = user.ProviderUserKey AndAlso Not t.Finished AndAlso t.AgendaId = p.Id Select New TaskListModel With {.Id = t.Id, .ProjectName = t.Project.Name, .Title = t.Title}), .AssignedTasks = (From t As Task In _model.Tasks Where t.OwnerId = user.ProviderUserKey AndAlso Not t.Finished AndAlso t.AssignedToId = p.Id Select New TaskListModel With {.Id = t.Id, .ProjectName = t.Project.Name, .Title = t.Title})}).FirstOrDefault
    End Function

    Public Function GetProfileForUser() As ProfileModel
        Dim person As Person = GetPersonForUser()

        Return New ProfileModel With {.Email = person.Email, .Name = person.Name, .Emails = (From e In _model.Emails Where e.OwnerId = person.UserId Order By e.Email Select New EmailModel With {.Id = e.Id, .Email = e.Email, .Confirmed = e.Confirmed}), .APIKey = person.OwnerId}
    End Function

    Function GetPeopleForUser() As IQueryable(Of PersonListModel)
        Dim user As MembershipUser = Membership.GetUser()
        Return From p In _model.People Where p.OwnerId = user.ProviderUserKey And p.OwnerId <> p.UserId Select New PersonListModel() With {.Id = p.Id, .Name = p.Name}
    End Function

    Function SearchPeopleForUser(query As String) As IQueryable(Of PersonModel)
        Dim user As MembershipUser = Membership.GetUser()
        Return From p In _model.People Where p.OwnerId = user.ProviderUserKey And p.OwnerId <> p.UserId And (p.Name.Contains(query) Or p.Email.Contains(query)) Select New PersonModel() With {.Id = p.Id, .Name = p.Name, .Email = p.Email, .HasAccount = p.UserId <> Guid.Empty}
    End Function

    Function CreatePerson(name As String, email As String) As PersonModel
        Dim member As MembershipUser = Membership.GetUser()

        Dim p As New Person With {.Name = name, .Email = email, .OwnerId = member.ProviderUserKey}

        _model.People.AddObject(p)
        _model.SaveChanges()

        Return New PersonModel() With {.Id = p.Id, .Name = p.Name, .Email = p.Email}
    End Function

    Function GetAssignablesForUser() As IQueryable(Of PersonListModel)
        Dim user As MembershipUser = Membership.GetUser()
        Return From p In _model.People Join e In _model.Emails On p.Email Equals e.Email Where p.UserId <> user.ProviderUserKey And e.OwnerId = user.ProviderUserKey Select New PersonListModel() With {.Id = p.Id, .Name = p.Name}
    End Function

    Sub AssignUser(Id As Integer)
        Dim user As MembershipUser = Membership.GetUser()
        Dim person As Person = (From p In _model.People Join e In _model.Emails On p.Email Equals e.Email Where p.UserId <> user.ProviderUserKey And e.OwnerId = user.ProviderUserKey And p.Id = Id Select p).FirstOrDefault

        If person IsNot Nothing Then
            person.UserId = user.ProviderUserKey
            _model.SaveChanges()
        End If
    End Sub

    Function GetAgendaPeopleForUser() As IQueryable(Of PersonListModel)
        Dim user As MembershipUser = Membership.GetUser()
        Return From t In _model.Tasks Where t.OwnerId = user.ProviderUserKey And Not t.Finished And (t.AgendaTo IsNot Nothing Or t.AssignedTo IsNot Nothing) Group By p = If(t.AgendaTo, t.AssignedTo) Into Group Select New PersonListModel() With {.Id = p.Id, .Name = p.Name, .AgendaTasks = Group.Count(Function(t) t.AgendaTo IsNot Nothing), .DelegatedTasks = Group.Count(Function(t) t.AssignedTo IsNot Nothing)}
    End Function

    Sub UpdatePerson(id As Integer, name As String, email As String)
        Dim user As MembershipUser = Membership.GetUser()
        Dim person As Person = (From p In _model.People Where p.OwnerId = user.ProviderUserKey And p.Id = id Select p).FirstOrDefault

        If person IsNot Nothing Then
            person.Name = name
            person.Email = email
            _model.SaveChanges()
        End If
    End Sub
End Class