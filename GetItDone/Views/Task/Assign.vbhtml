@ModelType GetItDone.AssignTaskModel

@Code
    ViewData("Title") = "Assign"
End Code

<h2>Delegate</h2>

<div class="info">This is something someone else should do. Find the right person to do it.</div>

<div id="thebigquestion">Who should '@Model.Title'?</div>

@Using Html.BeginForm
    @<div>Choose from your existing contacts: @Html.DropDownListFor(Function(t) t.AssignToId, ViewBag.Assignables)</div>
    @<div>Or write a new name here: @Html.TextBoxFor(Function(t) t.AssignToName)</div>
    @<input type="submit" value="Delegate" />
End Using
