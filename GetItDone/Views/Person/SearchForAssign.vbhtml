@ModelType IEnumerable(Of GetItDone.PersonModel)

@For Each person In Model
    @<div class="person" data-id="@person.Id">
        <div class="name">@person.Name</div>
        <div class="email">@person.Email</div>
    </div>
Next
