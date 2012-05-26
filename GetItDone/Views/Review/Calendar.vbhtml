@ModelType IQueryable(Of GetItDone.CalendarTaskListModel)

@Code
    ViewData("Title") = "Calendar"
End Code

<h2>Calendar</h2>

<div class="info">
    Look over your calendar for the past and next week and collect any additional actions related to these entries - preparation before meetings, gathering relevant tools, materials and so on.
</div>

<br />

@If Model.Any Then
    @Html.DisplayForModel()
Else
    @<p>Your calendar for the past and coming week is empty!</p>
End If

@Html.Partial("_CreateStuff", New GetItDone.CreateTaskModel())

@Html.ActionLink("I have collected everything", "Projects")
