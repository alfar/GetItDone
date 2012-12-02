@ModelType GetItDone.AssignTaskModel

@Code
    ViewData("Title") = "Assign"
End Code
 
<h2>Delegate</h2>

<div class="info">This is something someone else should do. Find the right person to do it.</div>

<div id="thebigquestion">Who should '@Model.Title'?</div>

@Using Html.BeginForm(New With {.From = ViewBag.From})
    @<div class="smallcenter">
        <input type="text" id="NameLookup" class="bigtext" />
        @Html.HiddenFor(Function(t) t.Id)
        @Html.HiddenFor(Function(t) t.AssignToId)
    </div>
    @<div class="section left">
        <h3>@Html.RadioButtonFor(Function(t) t.AssignType, 0, New With {.Id = "AssignTypeChoose"}) @Html.Label("AssignTypeChoose", "Choose from your existing contacts")</h3>
        <div id="PeopleList" class="select"></div>
    </div>
    @<div class="section right">
        <h3>@Html.RadioButtonFor(Function(t) t.AssignType, 1, New With {.Id = "AssignTypeCreate"}) @Html.Label("AssignTypeCreate", "Create a new contact")</h3>
        <div class="editor-label">@Html.LabelFor(Function(t) t.AssignToName)</div>
        <div class="editor-field">@Html.TextBoxFor(Function(t) t.AssignToName)</div>
        <div class="editor-label">@Html.LabelFor(Function(t) t.AssignToEmail)</div>
        <div class="editor-field">@Html.TextBoxFor(Function(t) t.AssignToEmail)</div>
    </div>
    @<br style="clear: both;" />
    @<div class="editor-field">@Html.TextBoxFor(Function(t) t.Title)</div>
    @<div class="editor-field">@Html.EditorFor(Function(t) t.Notes)</div>

    @<div class="button send" data-sendtype="1" id="SendToAgenda" style="background-color: #007f00;">Put in agenda</div>
    @<div class="button send" data-sendtype="4" id="JustDelegate" style="background-color: #0000ff;">Put in delegated</div>
    @<div class="button send" data-sendtype="2" id="SendToInbox" style="background-color: #ff7f00;">Send to their inbox</div>
    @<div class="button send" data-sendtype="3" id="SendEmail" style="background-color: #ff0000;">Send email</div>
    @<br style="clear: both;"/>
    @Html.HiddenFor(Function(t) t.SendType)
End Using
<script type="text/javascript">
    var search_timeout = undefined;
    function pageInit() {
        $('#NameLookup').focus();

        function AdjustSendButtons() {
            if ($('#AssignTypeCreate:checked').val()) {
                if ($('#AssignToEmail').val().indexOf('@@') > -1) {
                    $('#SendEmail').show();
                } else {
                    $('#SendEmail').hide();
                }
                $('#JustDelegate').show();
                $('#SendToAgenda').show();
                $('#SendToInbox').hide();
            } else {
                if ($('#AssignToId').val() != '') {
                    if ($(selectedElement).attr('data-hasaccount') == 'True') {
                        $('#SendToInbox').show();
                    } else {
                        $('#SendToInbox').hide();
                    }
                    $('#SendEmail').show();
                    $('#SendToAgenda').show();
                    $('#JustDelegate').show();
                } else {
                    $('#SendToInbox').hide();
                    $('#SendEmail').hide();
                    $('#SendToAgenda').hide();
                    $('#JustDelegate').hide();
                }
            }
        }

        $('.send').on('click', function (evt) {
            $('#SendType').val($(evt.target).attr('data-sendtype'));
            $('form').submit();
        }).hide();

        $('#NameLookup').on('keyup', function () {
            var query = $(this).val();

            if (search_timeout != undefined) {
                clearTimeout(search_timeout);
            }

            if (query == '') {
                $('#PeopleList').html('');
            }
            else {
                search_timeout = setTimeout(function () {
                    search_timeout = undefined;
                    $.ajax({
                        method: 'POST',
                        url: '/Person/SearchForAssign',
                        data: { 'query': query },
                        success: function (data) {
                            $('#PeopleList').html(data);
                            if ($('#PeopleList ul').children().length == 0) {
                                $('#AssignToName').val(query);
                                $('#AssignTypeCreate').click();
                            }
                        }
                    });
                }, 500);
            }
        });

        var selectedElement = null;
        $('#PeopleList').on('click', '.person', function (event) {
            if (selectedElement !== null) {
                $(selectedElement).removeClass('selected');
            }
            selectedElement = $(event.target).parents('li').first();
            $(selectedElement).addClass('selected');
            $('#AssignToId').val(($(this).attr('data-id')));
            $('#AssignTypeChoose').click();
        });

        $('#AssignTypeChoose').on('click', AdjustSendButtons);
        $('#AssignTypeCreate').on('click', AdjustSendButtons);

        $('#AssignToEmail').on('keyup', AdjustSendButtons);
    };
</script>

