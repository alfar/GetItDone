@ModelType IEnumerable(Of GetItDone.TaskListModel)

@Code
    ViewData("Title") = "Completed"
End Code

<h2>Completed</h2>

@Html.DisplayForModel 

<br style="clear: both;" />

@Html.Partial("_CreateStuff", New GetItDone.CreateTaskModel())

@Html.ActionLink("I have collected everything", "Process")
