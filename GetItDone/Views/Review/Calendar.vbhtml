@ModelType IQueryable(Of GetItDone.CalendarTaskModel)

@Code
    ViewData("Title") = "Calendar"
End Code

<h2>Calendar</h2>

@For Each c As GetItDone.CalendarTaskModel In Model
    @<p>@c.DueDate.ToShortDateString() - @c.Title</p>
Next