Namespace GetItDone
    <Authorize()>
    Public Class PersonController
        Inherits GetItDone.GetToDoneControllerBase

        Private service As New PersonService(container)

        <OutputCache(NoStore:=True, Duration:=0, VaryByParam:="None")>
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

        Function Details(id As Integer) As ActionResult
            ViewBag.From = "Task:Index"
            Return View(service.GetPersonDetailsForUser(id))
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
