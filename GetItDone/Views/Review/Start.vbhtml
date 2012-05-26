@ModelType GetItDone.ReviewModel 
@Code
    ViewData("Title") = "Review"
End Code

<h2>Review</h2>

<div class="info">
    You've set aside the time to go through your lists. First of all, get everything off of your mind by collecting anything you can think of.
</div>

@Html.Partial("_CreateStuff", New GetItDone.CreateTaskModel())

@Html.ActionLink("I am through collecting", "Calendar")