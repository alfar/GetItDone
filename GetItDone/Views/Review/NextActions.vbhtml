@ModelType IQueryable(Of GetItDone.ContextReviewModel)

@Code
    ViewData("Title") = "Next Actions"
End Code

<h2>Next Actions</h2>

@Html.DisplayForModel

@Html.ActionLink("Complete review", "Finish")