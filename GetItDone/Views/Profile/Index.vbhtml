@ModelType GetItDone.ProfileModel

@Code
    ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<fieldset>
    <legend>ProfileModel</legend>

    <div class="display-label">Name</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Name)
    </div>

    <div class="display-label">Email</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Email)
    </div>
</fieldset>
<p>
    @*@Html.ActionLink("Edit", "Edit", New With {.id = Model.PrimaryKey}) |*@
    @Html.ActionLink("Back to List", "Index")
</p>
