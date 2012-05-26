@ModelType GetItDone.CreateProjectModel

@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

<script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @<fieldset>
        <legend>Project</legend>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Name)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Name)
            @Html.ValidationMessageFor(Function(model) model.Name)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.CreateNextAction)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.CreateNextAction)
            @Html.ValidationMessageFor(Function(model) model.CreateNextAction)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.NextAction)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.NextAction)
            @Html.ValidationMessageFor(Function(model) model.NextAction)
        </div>

        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>
