Namespace GetItDone
    <Authorize()>
    Public Class PersonController
        Inherits System.Web.Mvc.Controller

        Private _model As New TaskModelContainer()
        Private service As New PersonService(_model)

        Public Function SearchForAssign(query As String) As ActionResult
            Return PartialView(service.SearchPeopleForUser(query))
        End Function

        Function Index() As ActionResult
            Return View(service.GetPeopleForUser())
        End Function

        Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost()> _
        Function Create(input As CreatePersonModel) As ActionResult
            Try
                service.CreatePerson(input.Name, input.Email)
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        Function Edit(id As Integer) As ActionResult
            Return View(service.GetPersonForUser(id))
        End Function

        <HttpPost()> _
        Function Edit(input As PersonModel) As ActionResult
            Try
                service.UpdatePerson(input.Id, input.Name, input.Email)
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

    End Class
End Namespace
