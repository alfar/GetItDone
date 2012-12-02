@ModelType GetItDone.ProjectDetailModel

@Code
    ViewData("Title") = Model.Name 
End Code

@Using Html.BeginForm(New With {.From = ViewBag.From})
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
            <div class="info right">Why do you want to do this project? Ask yourself "why?" 5 times to uncover the purpose.</div>
            @Html.ValidationMessageFor(Function(model) model.Purpose)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Principles)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Principles)
            <div class="info right">What principles should guide you in this project? If you know with certainty that these principles will be followed, you should be ok to hand over the project to someone else.</div>
            @Html.ValidationMessageFor(Function(model) model.Principles)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Vision)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Vision)
            <div class="info right">What is the best possible outcome of this project? Describe the optimal product, how it is received by its audience, and how that makes you feel.</div>
            @Html.ValidationMessageFor(Function(model) model.Vision)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Brainstorm )
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Brainstorm )
            <div class="info right">Write down anything you can think of that relates to this project. Do not censor yourself. Everything you've thought once is something you don't need to think again, if only you write it down.</div>
            @Html.ValidationMessageFor(Function(model) model.Brainstorm )
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Organizing)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Organizing)
            <div class="info right">From the above, determine next actions and possible sub-projects. Determine who is responsible and possibly a deadline for each task.</div>
            @Html.ValidationMessageFor(Function(model) model.Organizing)
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
End Using
