@Imports StackExchange.Profiling
@ModelType GetItDone.DoTaskModel
@Code
    ViewData("Title") = "Do"
End Code
@Using MiniProfiler.Current.Step("Index")
@<div>
<h2>
    Do</h2>
<div class="info">
    Doing is what moves your next actions towards the desired state 'Done'. Filter your
    choices by what context you're currently in, and start doing them, one at a time.
    You may want to take into account the priority, the time available and your energy
    level as you pick.</div>
<div id="thebigquestion">
    What are you going to do next?</div>
<div class="filter">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="layout">
        <tr>
            <td width="50%" valign="top">
                <p>Contexts:</p>
                @Using Html.BeginForm()
                    For Each item In Model.Contexts
                    @<label for="@("context" & item.Id)" class="card" style="display: block;">
                        <input type="checkbox" value="@item.Id" id="@("context" & item.Id)" name="contextIds"@IIf(item.Active, " checked=""checked""", "") onchange="this.form.submit();" /> 
                        @item.Name
                    </label>
                    Next
                End Using
            </td>
            <td width="50%" valign="top">
                <p>People:</p>
                @Html.DisplayFor(Function(m) m.AgendaPeople)
            </td>
        </tr>
    </table>
</div>
<br style="clear: both" />
@If Model.CalendarTasks.Any Then
    @<h3>Your calendar:</h3>
    @Html.DisplayFor(Function(m) m.CalendarTasks)
End If
@If Model.Tasks.Any Then
    @<h3>Tasks based on your active context:</h3>
    @Html.DisplayFor(Function(m) m.Tasks, "~/Views/Shared/DisplayTemplates/TaskModel.vbhtml")
End If
@Code
    Bundles.Reference("/Scripts/Task/Basic.js")
End Code
</div>
End Using 