@ModelType GetItDone.ProcessTaskModel

@Code
    ViewData("Title") = "Process"
End Code

<h2>Process</h2>

<div class="info">You have stuff in your inbox. Now is the time to process it. Evaluate your stuff one item at a time, and decide where they should go.</div>

<div id="thebigquestion">What is '@Model.Title'?</div>

@Html.Partial("_ProcessStuff")