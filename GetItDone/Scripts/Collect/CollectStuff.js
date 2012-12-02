/// <reference path="/scripts/jquery-1.7.1.js" />
function clearTitle(model) {
    var projName = $('#originalProject').val();
    $('#project').html(projName != '' ? projName + ' - Esc to reset' : '&lt;No project&gt; - Alt+P to set');
    $('#ProjectName').val($('#originalProject').val());
    $('#Title').val($('#originalTitle').val()).focus().autocomplete("option", "disabled", $('#originalProject').val() != '');
}

$(document).ready(function () {
    $(document).keydown(function (event) {
        if (event.which == 27) {
            $('#ProjectName').val('');
            $('#project').html('&lt;No project&gt; - Alt+P to set');
            $('#Title').focus().autocomplete("option", "disabled", false);
            event.preventDefault();
        } else if ((event.which == 80) && event.altKey) {
            var pname = $('#Title').val();
            $('#ProjectName').val(pname);
            $('#project').html(pname + ' - Esc to reset');
            $('#Title').val('').focus().autocomplete("option", "disabled", true);
        }
    });
    $('#Title').select().focus().autocomplete({
        source: '/Project/Autocomplete',
        delay: 600,
        disabled: $('#originalProject').val() != '',
        select: function (event, ui) {
            $('#project').html(ui.item.label + ' - Esc to reset');
            $('#ProjectName').val(ui.item.label);
            $('#Title').val('').focus().autocomplete("option", "disabled", true);
            event.preventDefault();
        }
    });
});
