Namespace GetItDone.API
    Public Class TaskController
        Inherits System.Web.Mvc.Controller

        Private container As New TaskModelContainer()
        Private taskservice As New TaskService(container)

        '
        ' GET: /Task
        Function Index(userkey As String) As JsonResult    
            Return (Json(taskservice.GetTasksForUser(userkey), JsonRequestBehavior.AllowGet))
        End Function

        <AcceptVerbs("POST")> _
        Function Create(userkey As Guid, model As APICreateTaskModel) As JsonResult
            Return Json(taskservice.CreateTask(model.Title, "", userkey))
        End Function

        Public Function iCal(userkey As Guid) As ContentResult
            Dim cal As New DDay.iCal.iCalendar()

            For Each t As CalendarTaskListModel In New ReviewService(container).GetCalendarForUser(userkey)
                Dim ev As DDay.iCal.Event = cal.Create(Of DDay.iCal.Event)()
                ev.Start = New DDay.iCal.iCalDateTime(t.DueDate)
                ev.Summary = t.Title
                ev.Status = DDay.iCal.EventStatus.Confirmed
                ev.UID = t.Id
            Next

            Dim calSer As New DDay.iCal.Serialization.iCalendar.iCalendarSerializer(cal)

            Return Content(calSer.SerializeToString(cal), "text/calendar")
        End Function
    End Class
End Namespace
