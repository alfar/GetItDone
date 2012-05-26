@ModelType IEnumerable(Of GetItDone.PersonModel)

<ul>
@For Each person In Model
    @<li class="person" data-id="@person.Id" data-hasaccount="@person.HasAccount">
        <div class="name">@person.Name</div>
        <div class="email">@person.Email</div>
        @If person.HasAccount Then
        @<div class="command"><img src="/Content/themes/base/images/done.png" /></div>        
        End If
    </li>
Next
</ul>