@ModelType GetItDone.ProfileModel

@Code
    ViewData("Title") = "My profile"
End Code

<h2>My profile</h2>

<fieldset>
    <legend>Basic information</legend>

    <div class="display-label">Name</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Name)
    </div>

    <div class="display-label">E-mails</div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Emails)
        @Html.ActionLink("Add an e-mail address", "Create", "Email")
    </div>
</fieldset>
<p>
    <ul>
        <li>@Html.ActionLink("People", "Index", "Person")</li>
        <li>@Html.ActionLink("Contexts", "Index", "Context")</li>
        <li>@Html.ActionLink("Projects", "Index", "Project")</li>
    </ul>
</p>
