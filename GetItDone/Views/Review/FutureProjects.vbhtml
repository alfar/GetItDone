@ModelType IQueryable(Of GetItDone.FutureProjectListModel)

@Code
    ViewData("Title") = "Some day/Maybe"
End Code

<h2>Projects that you've marked as some day/maybe</h2>

@Html.DisplayForModel()

@Html.Partial("_CreateStuff", New GetItDone.CreateTaskModel())

@Html.ActionLink("I have collected everything", "Delegated")
