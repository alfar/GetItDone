@ModelType GetItDone.CreateContextModel
@Using Ajax.BeginForm(New AjaxOptions With {.InsertionMode = InsertionMode.InsertBefore, .UpdateTargetId = "newContext"})
    @Html.EditorFor(Function(model) model.Name)
    @<input type="submit" value="Create" />
    @Html.ValidationMessageFor(Function(model) model.Name)
End Using
