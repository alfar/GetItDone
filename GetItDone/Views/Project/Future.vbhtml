@ModelType IEnumerable(Of GetItDone.FutureProjectListModel)

@Code
    ViewData("Title") = "Some day/Maybe"
End Code

<h2>@Html.ActionLink("Projects", "Index") | Some day/Maybe</h2>

@Html.DisplayForModel()

<p style="clear: both;">
    @Html.ActionLink("Create New", "Create")
</p>
