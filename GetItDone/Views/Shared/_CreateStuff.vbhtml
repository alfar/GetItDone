@ModelType GetItDone.CreateTaskModel
<div class="huge-middle">
@Using Ajax.BeginForm("CreateStuff", "Task", New Object(), New AjaxOptions() With {.OnSuccess = "clearTitle"})
    @Html.EditorFor(Function(model) model.Title, "BigText")
    @<input type="submit" value="Create" />
End Using 
</div>

<script type="text/javascript">
    function clearTitle(model) {
        $('#Title').val('').focus();

    }
</script>