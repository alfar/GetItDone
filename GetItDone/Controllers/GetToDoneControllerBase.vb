Namespace GetItDone
    Public Class GetToDoneControllerBase
        Inherits System.Web.Mvc.Controller

        Public Sub New()
            Me.New(TaskModelFactory.GetModel())
        End Sub

        Public Sub New(taskService As ITaskService)
            Me.TestableTaskService = taskService
        End Sub

        Protected container As ITaskModelContainer
        Protected taskservice As TaskService
        Protected reviewservice As ReviewService
        Protected TestableTaskService As ITaskService

        Protected Overrides Sub OnActionExecuted(filterContext As System.Web.Mvc.ActionExecutedContext)
            If Membership.GetUser() IsNot Nothing Then
                If GetType(ViewResult).IsAssignableFrom(filterContext.Result.GetType()) Then
                    ViewBag.InboxItemCount = TestableTaskService.CountInboxItems()
                    Dim lastReview As FinishedReviewModel = reviewservice.GetLastReviewForUser()

                    If lastReview IsNot Nothing Then
                        ViewBag.ReviewNotification = reviewservice.GetLastReviewForUser().Finished.ToString("r")
                    Else
                        ViewBag.ReviewNotification = Membership.GetUser().CreationDate.ToString("r")
                    End If

                End If
                MyBase.OnActionExecuted(filterContext)
            End If
        End Sub
    End Class
End Namespace
