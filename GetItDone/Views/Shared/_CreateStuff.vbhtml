@ModelType GetItDone.CreateTaskModel
<div class="huge-middle">
@Using Ajax.BeginForm("CreateStuff", "Task", New Object(), New AjaxOptions() With {.OnSuccess = "clearTitle"})
    @Html.EditorFor(Function(model) model.Title, "BigText")
    @Html.HiddenFor(Function(model) model.ProjectName)
    @<input type="submit" value="Create" />
End Using 
    <div>Project: <span id="project">@(If(String.IsNullOrWhiteSpace(Model.ProjectName), "<No project> - Alt+P to set", Model.ProjectName))</span></div>
</div>
@Html.HiddenFor(Function(m) m.Title, New With {.id = "originalTitle"})
@Html.HiddenFor(Function(m) m.ProjectName, New With {.id = "originalProject"})
@Code
    Bundles.Reference("/Scripts/Collect/CollectStuff.js")
End Code