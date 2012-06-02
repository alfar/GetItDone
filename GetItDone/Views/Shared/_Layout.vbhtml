<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link href="@Url.Content("~/Content/Site.less")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.base.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.core.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.button.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.selectable.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.theme.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/themes/base/jquery.ui.datepicker.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Scripts/jquery-1.7.1.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery-ui-1.8.17.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.unobtrusive-ajax.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.hotkeys-0.7.9.min.js")" type="text/javascript"></script>
<!--    <script src="@Url.Content("~/Scripts/gettodone.js")" type="text/javascript"></script>-->
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-4996356-6']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
</head>
	<body>
		<div class="page">
			<div id="header">
				<div id="menucontainer">
					<ul id="menu">
                        <li>@Html.ActionLink("Collect", "Collect", "Task")</li>
                        <li>@Html.ActionLink("Process", "Process", "Task")</li>
                        <li>@Html.ActionLink("Do", "Index", "Task")</li>
                        <li>@Html.ActionLink("Review", "Index", "Review")</li>
                        <li class="special">@Html.ActionLink("Offline", "PocketMod", "Task", Nothing, New With {.target = "_blank"})</li>
                        <li class="special">@Html.ActionLink("My profile", "Index", "Profile")</li>
					</ul>
				</div>
				<div id="title">
                    <h1>@Html.ActionLink("Get To Done!", "Index", "Home", New Object(), New Object())</h1>
				</div>
				<div id="logindisplay">
                    @Html.Partial("_LogOnPartial")
				</div>
			</div>
			<div id="main">
                @RenderBody()
			</div>
		</div>
	</body>
</html>
