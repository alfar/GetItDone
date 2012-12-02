@ModelType GetItDone.ProjectDetailModel

@Code
    ViewData("Title") = "Finish"
End Code

<h2>Finish</h2>

<h3>Are you sure you want to close this project?</h3>
<fieldset>
    <legend>Project</legend>

    <div class="display-label">Name</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Name)
    </div>

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

    <div class="display-label">CreatedDate</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.CreatedDate)
    </div>
</fieldset>
@Using Html.BeginForm(New With {.From = ViewBag.From})
    @<p>
        <input type="submit" value="I'm done with this" /> |
        @Html.ActionLink("Back to List", "Index")
    </p>
End Using
