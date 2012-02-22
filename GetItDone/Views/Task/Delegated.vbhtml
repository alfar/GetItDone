@ModelType IEnumerable(of GetItDone.AssigneeModel)

@Code
    ViewData("Title") = "Delegated tasks"
End Code

<h2>Delegated tasks</h2>

@For Each a In Model
    Dim assignee As GetItDone.AssigneeModel = a
    @<div class="card person">
        <h3 class="name">@assignee.Name</h3>
        <div class="tasks">
            @Html.DisplayFor(Function(ac) assignee.Tasks)
        </div>
    </div>
Next
<br style="clear: both;" />