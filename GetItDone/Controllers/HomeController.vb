Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private container As New TaskModelContainer()
    Private reviewservice As New ReviewService(container)

    Function Index() As ActionResult
        ViewData("Message") = "Welcome to ASP.NET MVC!"
        Return View()
    End Function

    <Authorize()> _
    Function Stuff() As ActionResult
        Return View()
    End Function

    <Authorize()> _
    Function Review() As ActionResult
        Return View(reviewservice.GetReviewDataForUser())
    End Function

    Function About() As ActionResult
        Return View()
    End Function
End Class
