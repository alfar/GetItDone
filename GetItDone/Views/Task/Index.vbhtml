@ModelType GetItDone.DoTaskModel
@Code
    ViewData("Title") = "Do"
End Code
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
    <div class="section left">
        <p>
            Active contexts:
            @Using Html.BeginForm()
                For Each item In Model.Contexts
                @<input type="checkbox" value="@item.Id" id="@("context" & item.Id)" name="contextIds"@IIf(item.Active, " checked=""checked""", "") onchange="this.form.submit();" /> @<label for="@("context" & item.Id)">@item.Name</label>@<br />
                Next
            End Using
        </p>
    </div>
    <div class="section right">
        <p>
            People with agendas:</p>
        <ul class="select">
            @For Each person In Model.AgendaPeople
                @<li>@Html.ActionLink(person.Name, "Agenda", New With {.Id = person.Id})</li>
            Next
        </ul>
    </div>
</div>
<br style="clear: both" />
@If Model.CalendarTasks.Any Then
    @<h3>
        Your calendar:</h3>
    @Html.DisplayFor(Function(m) m.CalendarTasks)
End If
@If Model.Tasks.Any Then
    @<h3>
        Tasks based on your active context:</h3>
    @Html.DisplayFor(Function(m) m.Tasks)
End If
<script type="text/javascript">
    function dropTask(id) {
        $('#task_' + id + ' td').fadeOut(2000, function () { $(this).remove(); });
    }

    $(function () {
        $('.task .title.editable').on('click', function (e) {
            var text = $(e.target).text();
            var edit = $('<input type="text" />').val(text);
            $(e.target).text('').append(edit);
            edit.select().focus().on('keydown', function (ev) {
                if (ev.keyCode == 27) {
                    $(e.target).text(text);
                } else if (ev.keyCode == 13) {
                    $.post('@Url.Action("Edit", "Task")', { 'Id': $(e.target).parent().attr('id'), 'Title': edit.val() }, function (data) {
                        $(e.target).text(edit.val()).effect('highlight', {}, 2000);
                    }, 'json');
                }
            });
        });

        $('.task .project.editable').on('click', function (e) {
            var text = $(e.target).text();
            var edit = $('<input type="text" />').val(text);
            $(e.target).text('').append(edit);
            edit.select().focus().autocomplete({ source: '@Url.Action("AutoComplete", "Project")' }).on('keydown', function (ev) {
                if (ev.keyCode == 27) {
                    $(e.target).text(text);
                } else if (ev.keyCode == 13) {
                    if (edit.val() != '') {
                        $.post('@Url.Action("AssignProject", "Task")', { 'id': $(e.target).parent().attr('id'), 'projectName': edit.val() }, function (data) {
                            $(e.target).text(edit.val()).removeClass('empty').effect('highlight', {}, 2000);
                        }, 'json');
                    }
                    else {
                        $.post('@Url.Action("AssignProject", "Task")', { 'id': $(e.target).parent().attr('id'), 'projectName': '' }, function (data) {
                            $(e.target).text('<No project>').addClass('empty').effect('highlight', {}, 2000);
                        }, 'json');
                        
                    }
                }
            });
        });
    });
</script>
