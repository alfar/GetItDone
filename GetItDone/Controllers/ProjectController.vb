Namespace GetItDone
    Public Class ProjectController
        Inherits System.Web.Mvc.Controller

        Private service As New ProjectService(New TaskModelContainer())

        '
        ' GET: /Project

        Function Index() As ActionResult
            Return View(service.GetProjectsForUser())
        End Function

        '
        ' GET: /Project/Details/5

        Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' GET: /Project/Create

        Function Create() As ActionResult
            Return View()
        End Function

        '
        ' POST: /Project/Create

        <HttpPost()> _
        Function Create(model As CreateProjectModel) As ActionResult
            Try
                If ModelState.IsValid Then
                    service.CreateProject(model.Name)
                    Return RedirectToAction("Index")
                Else
                    Return View()
                End If
            Catch
                Return View()
            End Try
        End Function
        
        '
        ' GET: /Project/Edit/5

        Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' POST: /Project/Edit/5

        <HttpPost> _
        Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add update logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        '
        ' GET: /Project/Delete/5

        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' POST: /Project/Delete/5

        <HttpPost> _
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
