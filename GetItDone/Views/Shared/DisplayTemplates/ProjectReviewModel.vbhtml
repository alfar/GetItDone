@ModelType GetItDone.ProjectReviewModel

<div class="card" id="@Model.Id">
    <h3 class="title">@Model.Name</h3>
    <div class="commands">
        <a class="command" href="@Url.Action("Demote", "Project", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Send to some day/maybe list" src="@Url.Content("~/Content/themes/base/images/future.png")" /></a>
        <a class="command" href="@Url.Action("Details", "Project", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="View" src="@Url.Content("~/Content/themes/base/images/view.png")" /></a>
        <a class="command" href="@Url.Action("Edit", "Project", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Edit" src="@Url.Content("~/Content/themes/base/images/edit.png")" /></a>
        <a class="command" href="@Url.Action("Finish", "Project", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Done" src="@Url.Content("~/Content/themes/base/images/done.png")" /></a>
        <a class="command" href="@Url.Action("Delete", "Project", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Delete" src="@Url.Content("~/Content/themes/base/images/delete.png")" /></a>
    </div>
    <div class="meta">@Html.LabelFor(Function(m) m.CreatedDate): @Html.DisplayFor(Function(m) m.CreatedDate)</div>
    <div class="progress">
        <div class="progressitem @(If(Model.HasPurpose, "done", ""))" title="Purpose"></div>
        <div class="progressitem @(If(Model.HasPrinciples , "done", ""))" title="Principles"></div>
        <div class="progressitem @(If(Model.HasVision , "done", ""))" title="Vision"></div>
        <div class="progressitem @(If(Model.HasBrainstorm , "done", ""))" title="Brainstorm"></div>
        <div class="progressitem @(If(Model.HasOrganizing , "done", ""))" title="Organizing"></div>
    </div>    
</div>