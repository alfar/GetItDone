@ModelType GetItDone.ContextReviewModel

<div class="card context">
    <div class="title">@Model.Name</div>
    @Html.DisplayFor(Function(m) m.Tasks)
</div>