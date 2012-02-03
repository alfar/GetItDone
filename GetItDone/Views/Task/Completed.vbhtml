@ModelType IEnumerable(Of GetItDone.TaskListModel)

@Code
    ViewData("Title") = "Completed"
End Code

<h2>Completed</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table>
    <tr>
        <th>
            Title
        </th>
        <th></th>
    </tr>

@For Each item In Model
    Dim currentItem = item
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.Title)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = currentItem.Id}) |
            @Html.ActionLink("Details", "Details", New With {.id = currentItem.Id}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = currentItem.Id})
        </td>
    </tr>
Next

</table>
