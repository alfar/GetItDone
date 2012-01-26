@ModelType GetItDone.CreateTaskModel

<script src="@Url.Content("~/Scripts/jquery-1.5.1.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @<div class="editor-field">
        @Html.EditorFor(Function(model) model.Title)
        @Html.ValidationMessageFor(Function(model) model.Title)
    </div>

    @<p>
        <input type="submit" value="Create" />
    </p>
End Using
