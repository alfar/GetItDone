@ModelType GetItDone.AssignTaskModel

@Code
    ViewData("Title") = "Assign"
End Code

<h2>Delegate</h2>

<div class="info">This is something someone else should do. Find the right person to do it.</div>

<div id="thebigquestion">Who should '@Model.Title'?</div>

@Using Html.BeginForm
    @<div>
        <input type="text" id="NameLookup" class="bigtext" />
        @Html.HiddenFor(Function(t) t.Id)
        @Html.HiddenFor(Function(t) t.AssignToId)
    </div>
    @<div class="section left">
        <h3>Choose from your existing contacts</h3>
        <div id="PeopleList"></div>
    </div>
    @<div class="section right">
        <h3>Create a new contact</h3>
        <div class="editor-label">@Html.LabelFor(Function(t) t.AssignToName)</div>
        <div class="editor-field">@Html.TextBoxFor(Function(t) t.AssignToName)</div>
        <div class="editor-label">@Html.LabelFor(Function(t) t.AssignToEmail)</div>
        <div class="editor-field">@Html.TextBoxFor(Function(t) t.AssignToEmail)</div>
    </div>
    
    @<div class="editor-field">@Html.TextBoxFor(Function(t) t.Title)</div>
    @<div class="editor-field">@Html.EditorFor(Function(t) t.Notes)</div>
    
    @<input type="submit" value="Delegate" />
End Using


<script type="text/javascript">
    var search_timeout = undefined;
    $(function () {
        $('#NameLookup').on('keyup', function () {
            var query = $(this).val();
            if (search_timeout != undefined) {
                clearTimeout(search_timeout);
            }

            search_timeout = setTimeout(function () {
                search_timeout = undefined;
                $.ajax({
                    method: 'POST',
                    url: '@Url.Action("SearchForAssign", "Person")',
                    data: { 'query': query },
                    success: function (data) {
                        $('#PeopleList').html(data);
                    }
                });
            }, 500);
        });

        $('#PeopleList').on('click', '.person', function (event) {
            $('#AssignToId').val(($(this).attr('data-id')));
            $('form').submit();
        });
    });
</script>