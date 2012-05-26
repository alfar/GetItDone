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
        Function Create(userkey As Guid, model As CreateTaskModel) As JsonResult
            Return Json(taskservice.CreateTask(model.Title, "", userkey))
        End Function

    End Class
End Namespace
