@Code
    ViewData("Title") = "Password Reset"
End Code

<h2>Your password has been reset</h2>

<p>An email containing a new temporary password has been sent to you.</p>

<p>@Html.ActionLink("Go to login page", "LogOn")</p>
