@ModelType GetItDone.FinishedProjectListModel

<div class="card" id="@Model.Id">
    <h3 class="title">@Model.Name</h3>
    <div class="meta">@Html.LabelFor(Function(m) m.CreatedDate): @Html.DisplayFor(Function(m) m.CreatedDate) - @Html.LabelFor(Function(m) m.DoneDate): @Html.DisplayFor(Function(m) m.DoneDate)</div>
</div>