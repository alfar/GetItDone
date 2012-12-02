@ModelType GetItDone.CreateTaskModel

@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @<fieldset>
        <legend>CreateTaskModel</legend>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Title)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Title)
            @Html.ValidationMessageFor(Function(model) model.Title)
        </div>

        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>
