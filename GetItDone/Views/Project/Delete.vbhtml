@ModelType GetItDone.ProjectDetailModel

@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>ProjectDetailModel</legend>

    <div class="display-label">Purpose</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Purpose)
    </div>

    <div class="display-label">Principles</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Principles)
    </div>

    <div class="display-label">Vision</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Vision)
    </div>

    <div class="display-label">Brainstorm</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Brainstorm)
    </div>

    <div class="display-label">Organizing</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Organizing)
    </div>

    <div class="display-label">Name</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Name)
    </div>

    <div class="display-label">CreatedDate</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.CreatedDate)
    </div>
</fieldset>
@Using Html.BeginForm()
    @<p>
        <input type="submit" value="Delete" /> |
        @Html.ActionLink("Back to List", "Index")
    </p>
End Using
