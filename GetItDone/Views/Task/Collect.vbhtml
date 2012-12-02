@ModelType IQueryable(Of GetItDone.AssignedTaskModel)
@Code
    ViewData("Title") = "Collect"
End Code

<h2>Collect</h2>
<div class="info">Here you can enter your stuff. Stuff is anything that is in your life and not in its right place. Often, it is something that needs to be done something about, but just as often it is something you shouldn't have to think about, but because you haven't decided what to do about it, it keeps lingering in your mind. Put it all here, one thing at a time, and feel how the worries disappear from your mind. You will not forget any of it, because it is stored in your system.</div>

@Html.Partial("_CreateStuff", New GetItDone.CreateTaskModel() With {.Title = ViewBag.TaskTitle, .ProjectName = ViewBag.ProjectName})
