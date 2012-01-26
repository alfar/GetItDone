Namespace GetItDone
    <Authorize()> _
    Public Class ContextController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Context

        Private service As New ContextService(New TaskModelContainer())

        Function Index() As ActionResult
            Return View(service.GetContextsForUser())
        End Function

        '
        ' GET: /Context/Details/5

        Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' GET: /Context/Create

        Function Create() As ActionResult
            Return PartialView()
        End Function

        '
        ' POST: /Context/Create

        <HttpPost()> _
        Function Create(TaskId As Integer, model As CreateContextModel) As ActionResult
            Try
                ViewBag.TaskId = TaskId
                Return PartialView("ListItem", service.CreateContext(model.Name))
            Catch
                Return View()
            End Try
        End Function

        '
        ' GET: /Context/Edit/5

        Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' POST: /Context/Edit/5

        <HttpPost()> _
        Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add update logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        '
        ' GET: /Context/Delete/5

        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' POST: /Context/Delete/5

        <HttpPost()> _
        Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add delete logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function
    End Class
End Namespace
