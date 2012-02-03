@ModelType GetItDone.TaskModel

@Code
    ViewData("Title") = "Finish"
End Code

<h2>Finish</h2>

<div class="info">Whenever you're done doing something, you should take a moment to reflect on what you've accomplished. Pat yourself on the back for a job well done. Then, think about whether doing this action caused new next actions to become apparent.</div>

<div id="thebigquestion">Are there any more actions related to '@Model.Title'?</div>

@Html.Partial("_CreateStuff", New GetItDone.CreateTaskModel With {.Title = Model.Title})

<div class="huge-middle">
@Using Html.BeginForm()
    @<input type="submit" value="No, I'm done" />
End Using
</div>
