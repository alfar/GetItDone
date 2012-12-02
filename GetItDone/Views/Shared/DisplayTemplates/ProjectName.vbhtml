@ModelType String
<div class="project @(If(String.IsNullOrEmpty(Model), " empty", ""))">@(If(String.IsNullOrEmpty(Model), "<No project>", Model))</div>