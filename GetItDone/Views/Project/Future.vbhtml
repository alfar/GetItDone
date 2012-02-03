@ModelType IEnumerable(Of GetItDone.ProjectModel)

@Code
    ViewData("Title") = "Some day/Maybe"
End Code

<h2>Some day/Maybe</h2>

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
            @Html.ActionLink("Edit", "Edit", New With {.id = currentItem.Id}) |
            @Html.ActionLink("Details", "Details", New With {.id = currentItem.Id}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = currentItem.Id})
        </td>
    </tr>
Next

</table>
