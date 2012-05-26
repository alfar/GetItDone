@ModelType String
<div class="project editable @(If(String.IsNullOrEmpty(Model), " empty", ""))">@(If(String.IsNullOrEmpty(Model), "<No project>", Model))</div>