@ModelType GetItDone.FutureProjectListModel

<div class="card" id="@Model.Id">
    <h3 class="title">@Model.Name</h3>
    <div class="commands">
        <a class="command" href="@Url.Action("Promote", "Project", New With {.id = Model.Id})"><img alt="Promote to active project" src="@Url.Content("~/Content/themes/base/images/promote.png")" /></a>
        <a class="command" href="@Url.Action("Details", "Project", New With {.id = Model.Id})"><img alt="View" src="@Url.Content("~/Content/themes/base/images/view.png")" /></a>
        <a class="command" href="@Url.Action("Edit", "Project", New With {.id = Model.Id})"><img alt="Edit" src="@Url.Content("~/Content/themes/base/images/edit.png")" /></a>
        <a class="command" href="@Url.Action("Finish", "Project", New With {.id = Model.Id})"><img alt="Done" src="@Url.Content("~/Content/themes/base/images/done.png")" /></a>
        <a class="command" href="@Url.Action("Delete", "Project", New With {.id = Model.Id})"><img alt="Delete" src="@Url.Content("~/Content/themes/base/images/delete.png")" /></a>
    </div>
    <div class="meta">@Html.LabelFor(Function(m) m.CreatedDate): @Html.DisplayFor(Function(m) m.CreatedDate)</div>
</div>