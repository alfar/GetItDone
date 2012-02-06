﻿<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.base.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.core.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.button.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.selectable.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.theme.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.datepicker.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Scripts/jquery-1.5.1.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery-ui-1.8.11.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.unobtrusive-ajax.js")" type="text/javascript"></script>
</head>
<body>
    <div class="page">
        <div id="header">
            <div id="title">
                <h1>Get it done!</h1>
            </div>
            <div id="logindisplay">
                @Html.Partial("_LogOnPartial")
            </div>
            <div id="menucontainer">
                <ul id="menu">
                    <li>@Html.ActionLink("Collect", "Stuff", "Home")</li>
                    <li>@Html.ActionLink("Process", "Process", "Task")</li>
                    <li>@Html.ActionLink("Do", "Index", "Task")</li>
                    <li>@Html.ActionLink("Review", "Index", "Review")</li>
                    <li>@Html.ActionLink("Calendar", "Calendar", "Review")</li>
                    <li>@Html.ActionLink("Projects", "Index", "Project")</li>
                </ul>
            </div>
        </div>
        <div id="main">
            @RenderBody()
        </div>
        <div id="footer">
        </div>
    </div>
</body>
</html>
