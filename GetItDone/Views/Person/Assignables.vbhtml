@ModelType IQueryable(Of GetItDone.PersonListModel)

@Code
    ViewData("Title") = "Assignables"
End Code

<h2>Assignables</h2>

@For Each p In Model
    @<div>
        <span>@p.Name</span> <span>@Html.ActionLink("Accept", "Assign", New With {.Id = p.Id})</span>
    </div>
Next
