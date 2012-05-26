<p>Hello @ViewBag.Data.AssignToName</p>
<p>You have been assigned the following task by @ViewBag.Sender.Name</p>
<h1>@ViewBag.Data.Title</h1>
<div>@Html.Markdown(ViewBag.Data.Notes)</div>
<p>Please contact @ViewBag.Sender.Name at @ViewBag.Sender.Email when you are done, or if you have any questions about this task.</p>