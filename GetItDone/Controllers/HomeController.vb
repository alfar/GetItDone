Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private container As New TaskModelContainer()
    Private reviewservice As New ReviewService(container)

    Function Index() As ActionResult
        ViewBag.PeopleCount = Membership.GetAllUsers().Count

        If Request.IsAuthenticated Then
            Dim ps As New PersonService(New TaskModelContainer())
            ps.GetPersonForUser()
        End If

        Return View()
    End Function

    Function About() As ActionResult
        Return View()
    End Function
End Class
