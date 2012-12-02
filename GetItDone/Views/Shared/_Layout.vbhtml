@Imports StackExchange.Profiling
@Using MiniProfiler.Current.Step("Presentation")
    Cassette.Views.Bundles.Reference ("/Content/Site.less")
    Cassette.Views.Bundles.Reference("/Content/themes/base/jquery.ui.base.css")
    Cassette.Views.Bundles.Reference("/Content/themes/base/jquery.ui.core.css")
    Cassette.Views.Bundles.Reference("/Content/themes/base/jquery.ui.button.css")
    Cassette.Views.Bundles.Reference("/Content/themes/base/jquery.ui.selectable.css")
    Cassette.Views.Bundles.Reference("/Content/themes/base/jquery.ui.theme.css")
    Cassette.Views.Bundles.Reference("/Content/themes/base/jquery.ui.datepicker.css")
    Cassette.Views.Bundles.Reference("/Scripts/jquery-1.7.1.js")
    Cassette.Views.Bundles.Reference("/Scripts/jquery-ui-1.8.17.js")
    Cassette.Views.Bundles.Reference("/Scripts/jquery.unobtrusive-ajax.js")
    Cassette.Views.Bundles.Reference("/Scripts/GoogleAnalytics.js")
@<!DOCTYPE html>
@<html>
<head>
    <title>@ViewData("Title")</title>
    @Bundles.RenderStylesheets()
    <link href="/favicon.ico" rel="icon" type="image/x-icon" />
</head>
	<body>
		<div class="page">
			<div id="header">
				<div id="menucontainer">
					<ul id="menu">
                        <li>@Html.ActionLink("Collect", "Collect", "Task")</li>
                        <li id="ProcessTab">@Html.ActionLink("Process", "Process", "Task")</li>
                        <li>@Html.ActionLink("Do", "Index", "Task")</li>
                        <li id="ReviewTab">@Html.ActionLink("Review", "Index", "Review")</li>
                        <li class="special">@Html.ActionLink("Projects", "Index", "Project")</li>
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
        @Html.Hidden("InboxItemCount", ViewBag.InboxItemCount)
        @Html.Hidden("ReviewNotification", ViewBag.ReviewNotification)
        @Bundles.RenderScripts()
        <script type="text/javascript">
            var uvOptions = {};
            (function () {
                var uv = document.createElement('script'); uv.type = 'text/javascript'; uv.async = true;
                uv.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'widget.uservoice.com/vwducbfNiqlPTFUjgMWQA.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(uv, s);
            })();
        </script>
	</body>
</html>
End Using
    