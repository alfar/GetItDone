@ModelType GetItDone.EmailModel
<div class="object email"><div class="title">@Model.Email</div>
    <div class="commands">
            <a class="command" href="@Url.Action("Delete", "Email", New With {.id = Model.Id, .From = ViewBag.From})"><img alt="Delete" src="@Url.Content("~/Content/themes/base/images/delete.png")" /></a>
@If Model.Confirmed Then
    @<img src="@Url.Content("~/Content/themes/base/images/done.png")" alt="Confirmed" />
Else
    @<img src="@Url.Content("~/Content/themes/base/images/done.png")" style="opacity: 0.2" alt="Not confirmed" />    
End If
</div></div>