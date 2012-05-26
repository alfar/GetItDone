@ModelType GetItDone.CreateTaskModel
<div class="huge-middle">
@Using Ajax.BeginForm("CreateStuff", "Task", New Object(), New AjaxOptions() With {.OnSuccess = "clearTitle"})
    @Html.EditorFor(Function(model) model.Title, "BigText")
    @<input type="submit" value="Create" />
End Using 
</div>

<script type="text/javascript">
    var origTitle = '';

    function clearTitle(model) {
        $('#Title').val(origTitle).focus();
    }

    $(function () {
        origTitle = $('<div />').html('@Model.Title').text();
    });

    $(document).ready(function () {
        $('#Title').select().focus().autocomplete({ source: '@Url.Action("AutoComplete", "Project")' });
    });
</script>