@ModelType IEnumerable(Of GetItDone.PersonListModel)

@Code
    ViewData("Title") = "People"
End Code

<h2>People</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table>
    <tr>
        <th>
            Name
        </th>
        <th></th>
    </tr>

@For Each item In Model
    Dim currentItem = item
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.Name)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = currentItem.Id})
            @Html.ActionLink("Delete", "Delete", New With {.id = currentItem.Id})
        </td>
    </tr>
Next

</table>
