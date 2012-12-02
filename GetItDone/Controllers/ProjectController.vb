Namespace GetItDone
    <Authorize()>
    Public Class ProjectController
        Inherits GetItDone.GetToDoneControllerBase

        Private service As New ProjectService(container)

        Private Function RedirectBack(From As String) As RedirectToRouteResult
            Dim parts() As String = From.Split(":"c)

            If parts.Length = 2 Then
                Return RedirectToAction(parts(1), parts(0))
            Else
                Return RedirectToAction(parts(0))
            End If
        End Function


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
        Function Create(model As CreateProjectModel, Optional From As String = "Task:Process") As ActionResult
            Try
                If ModelState.IsValid Then
                    Dim project As ProjectModel = service.CreateProject(model.Name)

                    If model.CreateNextAction Then
                        If model.NextActionId.HasValue Then
                            taskservice.UpdateTask(model.NextActionId.Value, model.NextAction, project.Id, Nothing)
                        Else
                            Dim task As TaskListModel = taskservice.CreateTask(model.NextAction, "", Membership.GetUser().ProviderUserKey)
                            taskservice.AssignProject(task.Id, project.Id)
                        End If
                    ElseIf model.NextActionId.HasValue Then
                        taskservice.DeleteTask(model.NextActionId.Value)
                    End If

                    Return RedirectBack(From)
                Else
                    Return View(model)
                End If
            Catch
                Return View(model)
            End Try
        End Function

        Function CreateFromStuff(id As Integer, Optional From As String = "Task:Process") As ActionResult
            Dim task As TaskModel = taskservice.GetTaskForUser(id)
            If task IsNot Nothing Then
                ViewBag.From = [From]
                Return View(New CreateProjectModel() With {.Name = task.Title, .CreateNextAction = True, .NextAction = task.Title, .NextActionId = id})
            Else
                Return RedirectBack(From)
            End If
        End Function

        '
        ' GET: /Project/Edit/5

        Function Edit(ByVal id As Integer, Optional From As String = "Project:Index") As ActionResult
            ViewBag.From = From
            Return View(service.GetProjectForUser(id))
        End Function

        '
        ' POST: /Project/Edit/5

        <HttpPost()> _
        Function Edit(ByVal id As Integer, data As ProjectDetailModel, Optional From As String = "Project:Index") As ActionResult
            Try
                If ModelState.IsValid Then
                    service.UpdateProject(id, data)

                    Return RedirectBack(From)
                End If
            Catch
            End Try

            Return View(service.GetProjectForUser(id))
        End Function

        Function Finish(id As Integer, Optional From As String = "Project:Index") As ActionResult
            Return View(service.GetProjectForUser(id))
        End Function

        <HttpPost()> _
        Function Finish(ByVal id As Integer, ByVal collection As FormCollection, Optional From As String = "Project:Index") As ActionResult
            Try
                service.FinishProject(id)
                Return RedirectBack(From)
            Catch
                Return View()
            End Try
        End Function

        '
        ' GET: /Project/Delete/5

        Function Delete(ByVal id As Integer, Optional From As String = "Project:Index") As ActionResult
            ViewBag.From = From
            Return View(service.GetProjectForUser(id))
        End Function

        '
        ' POST: /Project/Delete/5

        <HttpPost()> _
        Function Delete(ByVal id As Integer, cleanTasks As Boolean, ByVal collection As FormCollection, Optional From As String = "Project:Index") As ActionResult
            Try
                service.DeleteProject(id, cleanTasks)

                Return RedirectBack(From)
            Catch
                Return View()
            End Try
        End Function

        Function Demote(id As Integer, Optional From As String = "Project:Index")
            service.DemoteProject(id)
            Return RedirectBack(From)
        End Function

        Function Promote(id As Integer, Optional From As String = "Project:Future")
            service.PromoteProject(id)
            Return RedirectBack(From)
        End Function

        Function AutoComplete(term As String) As ActionResult
            Return Json((From p In service.GetProjectsForUser() Where p.Name.StartsWith(term) Select p.Name).ToArray(), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace
