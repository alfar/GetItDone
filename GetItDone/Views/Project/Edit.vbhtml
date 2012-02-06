@ModelType GetItDone.ProjectDetailModel

@Code
    ViewData("Title") = Model.Name 
End Code

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @<fieldset>
        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Name)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Name)
            @Html.ValidationMessageFor(Function(model) model.Name)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Purpose)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Purpose)
            @Html.ValidationMessageFor(Function(model) model.Purpose)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Principles)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Principles)
            @Html.ValidationMessageFor(Function(model) model.Principles)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Vision)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Vision)
            @Html.ValidationMessageFor(Function(model) model.Vision)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Brainstorm )
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Brainstorm )
            @Html.ValidationMessageFor(Function(model) model.Brainstorm )
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Organizing)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Organizing)
            @Html.ValidationMessageFor(Function(model) model.Organizing)
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
End Using
