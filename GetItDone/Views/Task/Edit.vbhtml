@ModelType GetItDone.TaskModel

@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @Html.Hidden("From", ViewBag.From)
    @<fieldset>
        <legend>TaskModel</legend>

        @Html.HiddenFor(Function(model) model.Id)

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Title)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Title)
            @Html.ValidationMessageFor(Function(model) model.Title)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.ProjectName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.ProjectName)
            @Html.ValidationMessageFor(Function(model) model.ProjectName)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Notes)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Notes)
            @Html.ValidationMessageFor(Function(model) model.Notes)
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

<script runat="server">
    function pageInit() {
        $('#ProjectName').autocomplete({
            source: '/Project/Autocomplete'
        });
    }
</script>