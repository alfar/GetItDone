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
End Class

Public Class PersonModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property Email As String
End Class

Public Class PersonListModel
    Public Property Id As Integer
    Public Property Name As String
End Class

Public Class PersonService
    Private _model As TaskModelContainer

    Public Sub New(model As TaskModelContainer)
        _model = model
    End Sub

    Public Function GetPersonForUser() As Person
        Dim user As MembershipUser = Membership.GetUser()
        Dim person As Person = _model.People.Where(Function(p) p.UserId = user.ProviderUserKey).FirstOrDefault

        If person Is Nothing Then
            person = New Person()
            person.UserId = user.ProviderUserKey
            person.Email = user.Email
            person.Name = user.UserName

            _model.People.AddObject(person)

            _model.SaveChanges()
        End If

        Return person
    End Function

    Public Function GetProfileForUser() As ProfileModel
        Dim person As Person = GetPersonForUser()
        Return New ProfileModel With {.Email = Person.Email, .Name = Person.Name}
    End Function

    Function GetPeopleForUser() As IQueryable(Of PersonListModel)
        Dim user As MembershipUser = Membership.GetUser()
        Return From p In _model.People Where p.OwnerId = user.ProviderUserKey And p.OwnerId <> p.UserId Select New PersonListModel() With {.Id = p.Id, .Name = p.Name}
    End Function

    Function CreatePerson(name As String) As PersonListModel
        Dim member As MembershipUser = Membership.GetUser()

        Dim p As New Person With {.Name = name, .OwnerId = member.ProviderUserKey}

        _model.People.AddObject(p)
        _model.SaveChanges()

        Return New PersonListModel() With {.Id = p.Id, .Name = p.Name}

    End Function

End Class