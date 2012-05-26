@ModelType GetItDone.ProcessTaskModel 

@Code
    ViewData("Title") = "Process"
End Code

<h2>Process</h2>

<div class="info">You have stuff in your inbox. Now is the time to process it. Evaluate your stuff one item at a time, and decide where they should go.</div>

<div id="thebigquestion">What is '@Model.Title'?</div>
<table class="workflow" border="0">
		<tr><td colspan="13"></td><td rowspan="7" class="contexts">
        <ul>
        @For Each ctx As GetItDone.ContextListModel In Model.Contexts
            @<li>@Html.ActionLink(ctx.Name, "AssignContext", New With {.Id = Model.Id, .Context = ctx.Id})</li>
        Next
            <li>@Using Html.BeginForm("AssignNewContext", "Task", New With {.Id = Model.Id})
                    @Html.TextBox("contextName")
                    @<input type="submit" value="New context" />
                End Using
            </li>
        </ul>
    </td></tr>
    <tr><td rowspan="2"></td><td class="goal">@Html.ActionLink("Some day/Maybe", "FutureProject", New With {.Id = Model.Id})</td><td></td><td class="goal">Reference</td><td rowspan="2"></td><td class="goal">@Html.ActionLink("Do it", "Finish", "Task", New With {.Id = Model.Id}, nothing)</td><td rowspan="2"></td><td class="goal">@Html.ActionLink("Delegate", "Assign", New With {.Id = Model.Id})</td><td rowspan="2" colspan="4"></td><td rowspan="2"></td></tr>
    <tr><td colspan="3" class="answer up">Kind of</td><td class="answer up">&lt; 2 min.</td><td class="answer up">No</td></tr>

    <tr><td class="answer right">Start</td><td colspan="3" class="question">Is it actionable?</td><td class="answer right">Yes</td><td class="question">What size is it?</td><td class="answer right">Good</td><td class="question">Are you the best person to do it?</td><td class="answer right">Yes</td><td class="question">Is timing important?</td><td class="answer right">No</td><td class="question">Where can you do it?</td><td class="answer right">Here:</td></tr>
    <tr><td rowspan="2"></td><td colspan="3" class="answer down">No</td><td></td><td class="answer down">Too big</td><td colspan="3" rowspan="2"></td><td class="answer down">Yes</td><td colspan="2" rowspan="2"></td></tr>

    <tr><td colspan="3" class="goal">@Html.ActionLink("Trash", "Delete", New With {.Id = Model.Id}, New With {.onclick = "return confirm('Are you sure this should go away forever?');"})</td><td></td><td class="goal">@Html.ActionLink("Project", "CreateFromStuff", "Project", New With {.Id = Model.Id, .Name = Model.Title}, Nothing)</td><td class="goal">@Html.ActionLink("Calendar", "Calendar", "Task", New With {.Id = Model.Id}, Nothing)</td></tr>
    <tr><td colspan="13"></td></tr>
</table>		
