@ModelType GetItDone.TaskListModel
    
<div class="task" id="@Model.Id">
    <div class="title">@Model.Title</div>
    <div class="commands">
        <a class="command" href="@Url.Action("Finish", New With {.id = Model.Id})"><img alt="Done" src="@Url.Content("~/Content/themes/base/images/done.png")" /></a>
        <a class="command" href="@Url.Action("Reprocess", New With {.id = Model.Id})"><img alt="Reprocess" src="@Url.Content("~/Content/themes/base/images/reprocess.png")" /></a>
        <a class="command" href="@Url.Action("Delete", New With {.id = Model.Id})"><img alt="Delete" src="@Url.Content("~/Content/themes/base/images/delete.png")" /></a>
    </div>
    <div class="project">@Model.ProjectName</div>
</div>
