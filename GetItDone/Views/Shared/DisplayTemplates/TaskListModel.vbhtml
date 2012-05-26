@ModelType GetItDone.TaskListModel
    
<div class="task" id="@Model.Id">
    <div class="title editable">@Model.Title</div>
    <div class="commands">
        <a class="command" href="@Url.Action("Finish", "Task", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Done" src="@Url.Content("~/Content/themes/base/images/done.png")" /></a>
        <a class="command" href="@Url.Action("Reprocess", "Task", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Reprocess" src="@Url.Content("~/Content/themes/base/images/reprocess.png")" /></a>
        <a class="command" href="@Url.Action("Delete", "Task", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Delete" src="@Url.Content("~/Content/themes/base/images/delete.png")" /></a>
    </div>
    @Html.DisplayFor(function(m) m.ProjectName)
</div>
