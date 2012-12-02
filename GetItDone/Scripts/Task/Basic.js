/// <reference path="/scripts/jquery-1.7.1.js" />
function dropTask(id) {
    $('#task_' + id + ' td').fadeOut(2000, function () { $(this).remove(); });
};
