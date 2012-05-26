@ModelType GetItDone.AgendaTaskModel

@Code
    ViewData("Title") = "Agenda"
End Code

<h2>Agenda for @Model.Person.Name</h2>

@Html.DisplayFor(Function(m) m.Tasks)

