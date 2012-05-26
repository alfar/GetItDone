Imports Mvc.Mailer

Namespace GetItDone
    <Authorize()>
    Public Class TaskController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Task
        Private container As New TaskModelContainer()
        Private taskservice As New TaskService(container)
        Private contextservice As New ContextService(container)
        Private projectservice As New ProjectService(container)
        Private personService As New PersonService(container)

        Private Function RedirectBack(From As String) As RedirectToRouteResult
            Dim parts() As String = From.Split(":"c)

            If parts.Length = 2 Then
                Return RedirectToAction(parts(1), parts(0))
            Else
                Return RedirectToAction(parts(0))
            End If
        End Function

        Function Index(contextIds() As Integer) As ActionResult
            If contextIds IsNot Nothing Then
                contextservice.ToggleActiveContexts(contextIds)
            End If
            Return View(New DoTaskModel With {.Tasks = taskservice.GetTasksForUser(), .Contexts = contextservice.GetNonemptyContextsForUser(), .AgendaPeople = personService.GetAgendaPeopleForUser(), .CalendarTasks = taskservice.GetTodaysTasksForUser()})
        End Function

        Public Function Agenda(id As Integer) As ActionResult
            Return View(New AgendaTaskModel With {.Tasks = taskservice.GetAgendaTasksForUser(id), .Person = personService.GetPersonForUser(id)})
        End Function

        Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost()> _
        Function Create(model As CreateTaskModel) As ActionResult
            If ModelState.IsValid Then
                taskservice.CreateTask(model.Title, "", Membership.GetUser().ProviderUserKey)

                Return RedirectToAction("Index")
            End If

            Return View(model)
        End Function

        <HttpPost()> _
        Function CreateStuff(model As CreateTaskModel) As ActionResult
            If ModelState.IsValid Then
                taskservice.CreateTask(model.Title, "", Membership.GetUser().ProviderUserKey)

                Return Json(model)
            End If

            Return Json(Nothing)
        End Function

        Function Delete(id As Integer, Optional From As String = "Process") As ActionResult
            taskservice.DeleteTask(id)
            If Request.IsAjaxRequest() Then
                Return Me.Content(id.ToString())
            Else
                Return RedirectBack(From)
            End If
        End Function

        Function Process() As ActionResult
            Dim task = taskservice.GetNextTaskForProcessing()
            If task IsNot Nothing Then
                Return View(task)
            End If

            Return View("NothingToProcess")
        End Function

        Function AssignContext(id As Integer, context As Integer, Optional From As String = "Process") As ActionResult
            taskservice.AssignContext(id, context)
                           Return RedirectBack(From)

        End Function

        Function AssignNewContext(id As Integer, contextName As String, Optional From As String = "Process") As ActionResult
            taskservice.AssignContext(id, contextservice.CreateContext(contextName).Id)
                            Return RedirectBack(From)

        End Function

        Function FutureProject(id As Integer, Optional From As String = "Process") As ActionResult
            Dim task As TaskModel = taskservice.GetTaskForUser(id)
            projectservice.CreateProject(task.Title, True)

            taskservice.DeleteTask(id)
                           Return RedirectBack(From)

        End Function

        Function Assign(id As Integer, Optional From As String = "Process") As ActionResult
            'ViewBag.Assignables = New SelectList(personService.GetPeopleForUser(), "Id", "Name")
            Dim t As TaskModel = taskservice.GetTaskForUser(id)
            ViewBag.From = [From]
            Return View(New AssignTaskModel With {.Id = t.Id, .Title = t.Title, .Notes = t.Notes})
        End Function

        <HttpPost()>
        Function Assign(task As AssignTaskModel, Optional From As String = "Process") As ActionResult
            If ModelState.IsValid Then
                taskservice.UpdateTask(task.Id, task.Title, task.Notes)
                If task.AssignToId = 0 Then
                    task.AssignToId = personService.CreatePerson(task.AssignToName, task.AssignToEmail).Id
                Else
                    Dim pm As PersonModel = personService.GetPersonForUser(task.AssignToId)
                    task.AssignToName = pm.Name
                    task.AssignToEmail = pm.Email
                End If

                If task.SendType = 1 Then
                    taskservice.AssignAgenda(task.Id, task.AssignToId)
                ElseIf task.SendType = 2 Then
                    taskservice.AssignPerson(task.Id, task.AssignToId, True)
                ElseIf task.SendType = 3 Then
                    taskservice.AssignPerson(task.Id, task.AssignToId, False)

                    Dim mailer As New Mailers.TaskMailer(container)
                    mailer.Assign(task).Send()
                ElseIf task.SendType = 4 Then
                    taskservice.AssignPerson(task.Id, task.AssignToId, False)
                End If
            End If
                            Return RedirectBack(From)

        End Function

        Function Finish(id As Integer, Optional From As String = "Index") As ActionResult
            ViewBag.From = [From]
            Return View(taskservice.GetFinishingTask(id))
        End Function

        <HttpPost()>
        Function Finish(task As FinishTaskModel, Optional From As String = "Index") As ActionResult
            If ModelState.IsValid Then
                taskservice.FinishTask(task.Id)
            End If

            Return RedirectBack(From)

        End Function

        Function Calendar(id As Integer, Optional From As String = "Process") As ActionResult
            Dim t As TaskModel = taskservice.GetTaskForUser(id)
            ViewBag.From = [From]
            Return View(New CalendarTaskModel With {.Id = t.Id, .Title = t.Title, .Notes = t.Notes})
        End Function

        <HttpPost()>
        Function Calendar(task As CalendarTaskModel, Optional From As String = "Process") As ActionResult
            If ModelState.IsValid Then
                taskservice.PutInCalendar(task)
            End If

                           Return RedirectBack(From)

        End Function

        Function Completed() As ActionResult
            Return View(taskservice.GetFinishedTasksForUser())
        End Function

        Function Delegated() As ActionResult
            Return View(taskservice.GetDelegatedTasksForUser())
        End Function

        Function Reprocess(id As Integer, Optional From As String = "Process") As ActionResult
            taskservice.Reprocess(id)
            Return RedirectBack(From)
        End Function

        Function Collect() As ActionResult
            Return View()
        End Function

        <HttpPost()> _
        Function Edit(em As TaskListModel) As ActionResult
            If ModelState.IsValid Then
                taskservice.UpdateTask(em.Id, em.Title, Nothing)
            End If

            Return Json(True)
        End Function

        <HttpPost()> _
        Function AssignProject(id As Integer, projectName As String) As ActionResult
            If ModelState.IsValid Then
                If String.IsNullOrWhiteSpace(projectName) Then
                    taskservice.UnassignProject(id)
                Else
                    Dim p As ProjectModel = projectservice.GetProjectsForUser().FirstOrDefault(Function(pm) pm.Name = projectName)

                    If p Is Nothing Then
                        p = projectservice.CreateProject(projectName)
                    End If
                    taskservice.AssignProject(id, p.Id)
                End If
            End If

            Return Json(True)
        End Function
    End Class
End Namespace
