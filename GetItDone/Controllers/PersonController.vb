Namespace GetItDone
    <Authorize()>
    Public Class PersonController
        Inherits System.Web.Mvc.Controller

        Private service As New PersonService(New TaskModelContainer())

        Public Function SearchForAssign(query As String) As ActionResult
            Return PartialView(service.SearchPeopleForUser(query))
        End Function
    End Class
End Namespace
