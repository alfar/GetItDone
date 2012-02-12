Namespace GetItDone
    <Authorize()>
    Public Class ProjectController
        Inherits System.Web.Mvc.Controller

        Private container As New TaskModelContainer()
        Private service As New ProjectService(container)
        Private taskservice As New TaskService(container)

        '
        ' GET: /Project

        Function Index() As ActionResult
            Return View(service.GetProjectsForUser())
        End Function

        Function Future() As ActionResult
            Return View(service.GetFutureProjectsForUser())
        End Function

        '
        ' GET: /Project/Details/5

        Function Details(ByVal id As Integer) As ActionResult
            Return View(service.GetProjectForUser(id))
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
                    Dim project As ProjectModel = service.CreateProject(model.Name)

                    If model.CreateNextAction Then
                        If model.NextActionId.HasValue Then
                            taskservice.UpdateTask(model.NextActionId.Value, model.NextAction)
                            taskservice.AssignProject(model.NextActionId.Value, project.Id)
                        Else
                            Dim task As TaskListModel = taskservice.CreateTask(model.NextAction, Membership.GetUser().ProviderUserKey)
                            taskservice.AssignProject(task.Id, project.Id)
                        End If
                    ElseIf model.NextActionId.HasValue Then
                        taskservice.DeleteTask(model.NextActionId.Value)
                    End If

                    Return RedirectToAction("Process", "Task", Nothing)
                Else
                    Return View(model)
                End If
            Catch
                Return View(model)
            End Try
        End Function

        Function CreateFromStuff(id As Integer) As ActionResult
            Dim task As TaskModel = taskservice.GetTaskForUser(id)
            If task IsNot Nothing Then
                Return View(New CreateProjectModel() With {.Name = task.Title, .CreateNextAction = True, .NextAction = task.Title, .NextActionId = id})
            Else
                Return RedirectToAction("Process", "Task", Nothing)
            End If
        End Function

        '
        ' GET: /Project/Edit/5

        Function Edit(ByVal id As Integer) As ActionResult
            Return View(service.GetProjectForUser(id))
        End Function

        '
        ' POST: /Project/Edit/5

        <HttpPost()> _
        Function Edit(ByVal id As Integer, data As ProjectDetailModel) As ActionResult
            Try
                If ModelState.IsValid Then
                    service.UpdateProject(id, data)

                    Return RedirectToAction("Index")
                End If
            Catch
            End Try

            Return View(service.GetProjectForUser(id))
        End Function

        '
        ' GET: /Project/Delete/5

        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' POST: /Project/Delete/5

        <HttpPost()> _
        Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add delete logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        Function AutoComplete(term As String) As ActionResult
            Return Json((From p In service.GetProjectsForUser() Where p.Name.StartsWith(term) Select p.Name).ToArray(), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace
