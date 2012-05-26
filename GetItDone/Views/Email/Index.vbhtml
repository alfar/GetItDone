@ModelType IEnumerable(Of GetItDone.EmailModel)

@Code
    ViewData("Title") = "Your email addresses"
End Code

<h2>Your email addresses</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
@Html.DisplayForModel()
