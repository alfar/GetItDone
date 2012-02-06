@ModelType GetItDone.ProjectDetailModel
 
@Code
    ViewData("Title") = Model.Name 
End Code

<h2>Project Details: @Model.Name</h2>

<h3>Purpose:</h3>
@Html.Markdown(Model.Purpose)

<h3>Principles:</h3>
@Html.Markdown(Model.Principles)

<h3>Outcome visioning:</h3>
@Html.Markdown(Model.Vision)

<h3>Brainstorming:</h3>
@Html.Markdown(Model.Brainstorm)

<h3>Organizing:</h3>
@Html.Markdown(Model.Organizing)

<h3>Next actions:</h3>
<ul>
@For Each na In Model.NextActions
    @<li>@na.Title</li>    
Next
</ul>