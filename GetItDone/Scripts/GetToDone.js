/// <reference path="jquery-1.7.1.js" />
$(function () {
    if (typeof pageInit != 'undefined' && $.isFunction(pageInit)) {
        pageInit();
    }

    // Get InboxItemCount
    var inboxItemCount = parseInt($('#InboxItemCount').val(), 10);

    if (inboxItemCount > 0) {
        $('#ProcessTab').children().first().append($('<span class="indicator">' + inboxItemCount + '</span>'));
    }

    var reviewNotification = new Date($('#ReviewNotification').val());

    if (new Date() - reviewNotification > 604800000) {
        $('#ReviewTab').children().first().append($('<span class="indicator" title="Your last review was on ' + reviewNotification.toLocaleString() + '">!</span>'));
    }
});