@ModelType GetItDone.ReviewInfo 
@Code
    ViewData("Title") = "Review"
End Code

<h2>Review</h2>

<div class="info">
    The weekly review should happen weekly. You will go through the following steps each week.
@If Model.LastReview Is Nothing Then
    @<text>You have not completed a review yet.</text>
Else
    @<text>You last completed a review on @Model.LastReview.Finished.</text>    
End If
</div>

<div>
@If Model.ActiveReview Is Nothing Then
    @:@Html.ActionLink("Start a new review", "Start", Nothing, New With {.class = "button", .style="background-color: #009900;"})
Else
    @<div>You started the active review on @(Model.ActiveReview.Started).</div>
End If
</div>
<br style="clear: both;" />

<ol>
    <li>@Html.ActionLink("Collect everything", "Start")</li>
    <li>@Html.ActionLink("Review your calendar", "Calendar")</li>
    <li>@Html.ActionLink("Review your projects", "Projects")</li>
    <li>@Html.ActionLink("Review your some day/maybe list", "FutureProjects")</li>
    <li>@Html.ActionLink("Review delegated actions", "Delegated")</li>
    <li>@Html.ActionLink("Review completed actions", "Completed")</li>
    <li>@Html.ActionLink("Process your inbox to zero", "Process")</li>    
    <li>@Html.ActionLink("Review your next actions", "NextActions")</li>    
</ol>