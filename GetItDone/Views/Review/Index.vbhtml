@Code
    ViewData("Title") = "Review"
End Code

<h2>Review</h2>

<div class="info">
    The weekly review should happen weekly. You will go through the following steps each week.
</div>

<ol>
    <li>@Html.ActionLink("Collect everything", "Stuff", "Home")</li>
    <li>@Html.ActionLink("Review your calendar", "Calendar")</li>
    <li>@Html.ActionLink("Review your projects", "Index", "Project")</li>
    <li>@Html.ActionLink("Review your some day/maybe list", "Future", "Project")</li>
    <li>@Html.ActionLink("Review completed actions", "Completed", "Task")</li>
    <li>@Html.ActionLink("Process your inbox to zero", "Process", "Task")</li>    
    <li>Review your next actions</li>
</ol>