@ModelType GetItDone.PersonDetailModel

@Code
    ViewData("Title") = "Details"
End Code

<h2>@Html.DisplayFor(Function(model) model.Name)</h2>
<p>@Html.DisplayFor(Function(model) model.Email)</p>

@If Model.AgendaTasks.ToList().Any() Then
@<h3>Agenda</h3>
@<div>
    @Html.DisplayFor(Function(model) model.AgendaTasks)
</div>
End If
    
@If Model.AssignedTasks.ToList().Any() Then
@<h3>Delegated</h3>                     
@<div>
    @Html.DisplayFor(Function(model) model.AssignedTasks)
</div>
End If    

<p>
    @Html.ActionLink("Edit", "Edit", New With {.id = Model.Id}) |
    @Html.ActionLink("Back to tasks", "Index", "Task", Nothing, Nothing)
</p>
