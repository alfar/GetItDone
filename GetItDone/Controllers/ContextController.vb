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
            Return View(service.GetContextForUser(id))
        End Function

        '
        ' GET: /Context/Create

        Function Create() As ActionResult
            Return View()
        End Function

        '
        ' POST: /Context/Create

        <HttpPost()> _
        Function Create(model As CreateContextModel) As ActionResult
            If ModelState.IsValid Then
                service.CreateContext(model.Name)
                Return RedirectToAction("Index")
            Else
                Return View()

            End If
        End Function

        '
        ' GET: /Context/Edit/5

        Function Edit(ByVal id As Integer) As ActionResult
            Return View(service.GetContextForUser(id))
        End Function

        '
        ' POST: /Context/Edit/5

        <HttpPost()> _
        Function Edit(ctx As ContextModel) As ActionResult
            Try
                If ModelState.IsValid Then
                    service.UpdateContext(ctx)
                End If

                Return RedirectToAction("Index")
            Catch
                Return View(ctx)
            End Try
        End Function

        '
        ' GET: /Context/Delete/5

        Function Delete(ByVal id As Integer) As ActionResult
            Return View(service.GetContextForUser(id))
        End Function

        '
        ' POST: /Context/Delete/5

        <HttpPost()> _
        Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                service.DeleteContext(id)

                Return RedirectToAction("Index")
            Catch
                Return View(service.GetContextForUser(id))
            End Try
        End Function
    End Class
End Namespace
