Namespace GetItDone
    <Authorize()>
    Public Class ReviewController
        Inherits GetItDone.GetToDoneControllerBase

        '
        ' GET: /Review

        Function Index() As ActionResult
            Return View(reviewservice.GetReviewInfoForUser())
        End Function

        Function Start() As ActionResult
            Dim r As ReviewModel = reviewservice.GetActiveReviewForUser()

            If r Is Nothing Then
                r = reviewservice.CreateReview()
            End If

            Return View(r)
        End Function

        Function Calendar() As ActionResult
            ViewBag.From = "Review:Calendar"
            Return View(reviewservice.GetCalendarForUser())
        End Function

        Function Projects() As ActionResult
            ViewBag.From = "Review:Projects"
            Return View(reviewservice.GetProjectInfo())
        End Function

        Function FutureProjects() As ActionResult
            ViewBag.From = "Review:FutureProjects"
            Return View(reviewservice.GetFutureInfo())
        End Function

        Function Delegated() As ActionResult
            ViewBag.From = "Review:Delegated"
            Return View(reviewservice.GetDelegatedTasks())
        End Function

        Function Completed() As ActionResult
            ViewBag.From = "Review:Completed"
            Return View(reviewservice.GetCompletedTasks())
        End Function

        Function Process() As ActionResult
            ViewBag.From = "Review:Process"
            Dim t As ProcessTaskModel = reviewservice.GetProcessTask()

            If t IsNot Nothing Then
                Return View(t)
            End If

            Return RedirectToAction("NextActions")
        End Function

        Function NextActions() As ActionResult
            ViewBag.From = "Review:NextActions"
            Return View(reviewservice.GetNextActions())
        End Function

        Function Finish() As ActionResult
            reviewservice.FinishReview()

            Return RedirectToAction("Index")
        End Function

    End Class
End Namespace
