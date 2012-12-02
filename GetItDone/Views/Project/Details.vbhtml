@ModelType GetItDone.ProjectDetailModel
 
@Code
    ViewData("Title") = Model.Name 
End Code

<h2>Project Details: @Model.Name</h2>

<h3>Purpose:</h3>
@Html.Markdown(Model.Purpose, Model.Id)

<h3>Principles:</h3>
@Html.Markdown(Model.Principles, Model.id)

<h3>Outcome visioning:</h3>
@Html.Markdown(Model.Vision, Model.id)

<h3>Brainstorming:</h3>
@Html.Markdown(Model.Brainstorm, Model.id)

<h3>Organizing:</h3>
@Html.Markdown(Model.Organizing, Model.id)

<h3>Next actions:</h3>
@Html.DisplayFor(Function(m) m.NextActions)
