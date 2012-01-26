@ModelType GetItDone.ReviewModel

@Code
    ViewData("Title") = "Review"
End Code

<h2>Review</h2>

<ul class="contextcards">
@For Each wf In Model.WaitingForPeople
    @<li>@wf.Name 
        <ul class="tasklist">
        @For Each t As GetItDone.TaskListModel In wf.Tasks
            @<li>@t.Title</li>        
        Next
        </ul>
    </li>    
Next
@If Model.UnactionableProjects.Any() Then
    @<li>Unactionable Projects
        <ul class="tasklist">
        @For Each p As GetItDone.ProjectModel In Model.UnactionableProjects
            @<li>@p.Name</li>
        Next
        </ul>
    </li>
End If
@For Each c As GetItDone.ContextReviewModel In Model.Contexts
    @<li>@c.Name
        <ul class="tasklist">
        @For Each t As GetItDone.TaskListModel In c.Tasks
            @<li>@t.Title</li>
        Next
        </ul>
    </li>
Next
</ul>
<div style="clear:both"></div>
