@ModelType GetItDone.DoTaskModel

@Code
    ViewData("Title") = "Do"
End Code

<h2>Do</h2>

<div class="info">Doing is what moves your next actions towards the desired state 'Done'. Filter your choices by what context you're currently in, and start doing them, one at a time. You may want to take into account the priority, the time available and your energy level as you pick.</div>


<div id="thebigquestion">What are you going to do next?</div>

<div class="filter">
<p>Active contexts: 
@Using Html.BeginForm()
    For Each item In Model.Contexts
        @<input type="checkbox" value="@item.Id" id="@("context" & item.Id)" name="contextIds"@IIf(item.Active, " checked=""checked""", "") onchange="this.form.submit();" /> @<label for="@("context" & item.Id)">@item.Name</label>@<br />
    Next    
end using
</p>
</div>

    @Html.DisplayFor(Function(m) m.Tasks)

<script type="text/javascript">
    function dropTask(id) {
        $('#task_' + id + ' td').fadeOut(2000, function () { $(this).remove(); });
    }
</script>
