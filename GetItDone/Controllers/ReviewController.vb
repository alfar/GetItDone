Namespace GetItDone
    <Authorize()>
    Public Class ReviewController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Review

        Private container As New TaskModelContainer()
        Private reviewservice As New ReviewService(container)

        Function Index() As ActionResult
            Return View()
        End Function

        Function Calendar() As ActionResult
            Return View(reviewservice.GetCalendarForUser())
        End Function

    End Class
End Namespace
