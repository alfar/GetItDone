@ModelType GetItDone.ReviewProjectInfo

@Code
    ViewData("Title") = "Projects"
End Code

@If Model.EmptyProjects.Any then
    @<h2>Projects that lack a next action</h2>

    @<div class="info">
        These projects lack a next action, meaning that you're probably not moving forward on them. For each, decide if they're actually still things you want to do, and if so, find the next action. If the next action is unclear, maybe you need to work more on your natural planning for that project.
    </div>

    @<br />

    @<text>@Html.DisplayFor(Function(m) m.EmptyProjects)</text>

End If

@If Model.ActiveProjects.Any Then
    @<h2>Active projects</h2>

    @<div class="info">
        These projects are active - you have at least one next action lined up for them. Still, you should consider whether or not each project is still valuable to you, and whether they would benefit from further work in the natural planning model.
    </div>
    
    @<br />
    
    @<text>@Html.DisplayFor(Function(m) m.ActiveProjects)</text>
End If

@If Model.FinishedProjects.Any Then
    @<h2>Completed projects</h2>

    @<div class="info">
        You have completed these projects since your last review. Collect any new actions that arise from your last look at each project.
    </div>
    
    @<br />
    
    @<text>@Html.DisplayFor(Function(m) m.FinishedProjects)</text>
End If

@Html.Partial("_CreateStuff", New GetItDone.CreateTaskModel())</text>

@Html.ActionLink("I have collected everything", "FutureProjects")
