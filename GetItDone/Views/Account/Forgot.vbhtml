@Code
    ViewData("Title") = "I forgot my password"
End Code

<h2>I forgot my password</h2>
@Using Html.BeginForm()
    @<div>
        <fieldset>
            <div class="editor-label">
                Enter your email address
            </div>
            <div class="editor-field">
                @Html.TextBox("email")
            </div>

            <p>
                <input type="submit" value="Request new password" />
            </p>
        </fieldset> 
    </div>
End Using