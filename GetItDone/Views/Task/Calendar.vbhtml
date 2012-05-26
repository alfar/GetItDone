@ModelType GetItDone.CalendarTaskModel

@Code
    ViewData("Title") = "Calendar"
End Code

<h2>Calendar</h2>

<script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>

@Using Html.BeginForm(New With {.From = ViewBag.From})
    @Html.ValidationSummary(True)
    @<fieldset>
        <legend>CalendarTaskModel</legend>

        @Html.HiddenFor(Function(model) model.Id)

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Title)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Title)
            @Html.ValidationMessageFor(Function(model) model.Title)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Notes)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Notes)
            @Html.ValidationMessageFor(Function(model) model.Notes)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.DueDate)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.DueDate)
            @Html.ValidationMessageFor(Function(model) model.DueDate)
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>
<script type="text/javascript">
    $(function () { $('.datefield').datepicker({dateFormat: 'dd-mm-yy'}); })
</script>