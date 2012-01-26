@ModelType GetItDone.ContextListModel
<li>@Html.ActionLink(Model.Name, "AssignContext", New With {.Id = ViewBag.TaskId, .Context = Model.Id})</li></li>
