@ModelType GetItDone.CreateEmailModel

@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @<div class="editor-label">
        @Html.LabelFor(Function(model) model.Email)
    </div>
    @<div class="editor-field">
        @Html.EditorFor(Function(model) model.Email)
        @Html.ValidationMessageFor(Function(model) model.Email)
    </div>
    @<p>
        <input type="submit" value="Create" />
    </p>
End Using

<div>
    @Html.ActionLink("Back to List", "Index", "Profile", Nothing, Nothing)
</div>
