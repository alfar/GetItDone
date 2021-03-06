﻿@ModelType GetItDone.LogOnModel

@Code
    ViewData("Title") = "Log On"
End Code

<h2>Log On</h2>
<p>
    Please enter your user name and password. @Html.ActionLink("Register", "Register") if you don't have an account.
</p>

@Html.ValidationSummary(True, "Login was unsuccessful. Please correct the errors and try again.")

@Using Html.BeginForm()
    @<div>
        <fieldset>
            <legend>Account Information</legend>

            <div class="editor-label">
                @Html.LabelFor(Function(m) m.UserName)
            </div>
            <div class="editor-field">
                @Html.TextBoxFor(Function(m) m.UserName)
                @Html.ValidationMessageFor(Function(m) m.UserName)
            </div>

            <div class="editor-label">
                @Html.LabelFor(Function(m) m.Password)
            </div>
            <div class="editor-field">
                @Html.PasswordFor(Function(m) m.Password)
                @Html.ValidationMessageFor(Function(m) m.Password)
            </div>

            <div class="editor-label">
                @Html.CheckBoxFor(Function(m) m.RememberMe)
                @Html.LabelFor(Function(m) m.RememberMe)
            </div>

            <p>
                <input type="submit" value="Log On" />
            </p>

            <p>
                @Html.ActionLink("Forgot your password?", "Forgot")
            </p>
        </fieldset>
    </div>
End Using
