@ModelType IEnumerable(Of GetItDone.ProjectListModel)

@Code
    ViewData("Title") = "Projects"
End Code

<h2>Projects | @Html.ActionLink("Some day/Maybe", "Future")</h2>

@Html.DisplayForModel()

<p style="clear: both;">
    @Html.ActionLink("Create New", "Create")
</p>
