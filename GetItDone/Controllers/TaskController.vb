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

        Function Index(contextIds() As Integer) As ActionResult
            If contextIds Is Nothing Then
                ViewBag.ActiveContexts = New List(Of Integer)()
                Return View(New DoTaskModel With {.Tasks = taskservice.GetTasksForUser(), .Contexts = contextservice.GetContextsForUser()})
            Else
                ViewBag.ActiveContexts = New List(Of Integer)(contextIds)
                Return View(New DoTaskModel With {.Tasks = taskservice.GetTasksForContexts(contextIds.ToArray()), .Contexts = contextservice.GetContextsForUser()})
            End If
        End Function

        Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost()> _
        Function Create(model As CreateTaskModel) As ActionResult
            If ModelState.IsValid Then
                taskservice.CreateTask(model.Title, Membership.GetUser().ProviderUserKey)

                Return RedirectToAction("Index")
            End If

            Return View(model)
        End Function

        <HttpPost()> _
        Function CreateStuff(model As CreateTaskModel) As ActionResult
            If ModelState.IsValid Then
                taskservice.CreateTask(model.Title, Membership.GetUser().ProviderUserKey)

                Return Json(model)
            End If

            Return Json(Nothing)
        End Function

        Function Delete(id As Integer) As ActionResult
            taskservice.DeleteTask(id)
            If Request.IsAjaxRequest() Then
                Return Me.Content(id.ToString())
            Else
                Return RedirectToAction("Process")
            End If
        End Function

        Function Process() As ActionResult
            Dim task = taskservice.GetNextTaskForProcessing()
            If task IsNot Nothing Then
                Return View(task)
            End If

            Return View("NothingToProcess")
        End Function

        Function AssignContext(id As Integer, context As Integer) As ActionResult
            taskservice.AssignContext(id, Context)
            Return RedirectToAction("Process")
        End Function

        Function AssignNewContext(id As Integer, contextName As String) As ActionResult
            taskservice.AssignContext(id, contextservice.CreateContext(contextName).Id)
            Return RedirectToAction("Process")
        End Function

        Function CreateProject(id As Integer, project As CreateProjectModel) As ActionResult
            projectservice.CreateProject(project.Name)

            taskservice.DeleteTask(id)
            Return RedirectToAction("Process")
        End Function

        Function Assign(id As Integer) As ActionResult
            ViewBag.Assignables = New SelectList(personService.GetPeopleForUser(), "Id", "Name")
            Dim t As TaskModel = taskservice.GetTaskForUser(id)
            Return View(New AssignTaskModel With {.Id = t.Id, .Title = t.Title, .Notes = t.Notes})
        End Function

        <HttpPost()>
        Function Assign(task As AssignTaskModel) As ActionResult
            If ModelState.IsValid Then
                If Not String.IsNullOrEmpty(task.AssignToName) Then
                    taskservice.AssignPerson(task.Id, personService.CreatePerson(task.AssignToName).Id)
                Else
                    taskservice.AssignPerson(task.Id, task.AssignToId)
                End If
            End If
            Return RedirectToAction("Process")
        End Function

        Function Finish(id As Integer) As ActionResult
            Return View(taskservice.GetFinishingTask(id))
        End Function

        <HttpPost()>
        Function Finish(task As FinishTaskModel) As ActionResult
            If ModelState.IsValid Then
                taskservice.FinishTask(task.id)
            End If

            Return RedirectToAction("Index")
        End Function

        Function Calendar(id As Integer) As ActionResult
            Dim t As TaskModel = taskservice.GetTaskForUser(id)
            Return View(New CalendarTaskModel With {.Id = t.Id, .Title = t.Title, .Notes = t.Notes})
        End Function

        <HttpPost()>
        Function Calendar(task As CalendarTaskModel) As ActionResult
            If ModelState.IsValid Then
                taskservice.PutInCalendar(task)
            End If

            Return RedirectToAction("Process")
        End Function

        Function Completed() As ActionResult
            Return View(taskservice.GetFinishedTasksForUser())
        End Function

        Function Reprocess(id As Integer) As ActionResult
            taskservice.Reprocess(id)
            Return RedirectToAction("Process")
        End Function
    End Class
End Namespace
