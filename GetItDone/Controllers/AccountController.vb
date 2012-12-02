Imports Mvc.Mailer
Imports System.Diagnostics.CodeAnalysis
Imports System.Security.Principal
Imports System.Web.Routing

Public Class AccountController
    Inherits GetItDone.GetToDoneControllerBase

    '
    ' GET: /Account/LogOn

    Public Function LogOn() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/LogOn

    <HttpPost()> _
    Public Function LogOn(ByVal model As LogOnModel, ByVal returnUrl As String) As ActionResult
        If ModelState.IsValid Then
            Dim mailUserName As String = Membership.GetUserNameByEmail(model.UserName)
            If mailUserName IsNot Nothing Then
                model.UserName = mailUserName
            End If

            If Membership.ValidateUser(model.UserName, model.Password) Then
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe)
                If Url.IsLocalUrl(returnUrl) AndAlso returnUrl.Length > 1 AndAlso returnUrl.StartsWith("/") _
                   AndAlso Not returnUrl.StartsWith("//") AndAlso Not returnUrl.StartsWith("/\\") Then
                    Return Redirect(returnUrl)
                Else
                    Return RedirectToAction("Index", "Home")
                End If
            Else
                ModelState.AddModelError("", "The user name or password provided is incorrect.")
            End If
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    '
    ' GET: /Account/LogOff

    Public Function LogOff() As ActionResult
        FormsAuthentication.SignOut()

        Return RedirectToAction("Index", "Home")
    End Function

    '
    ' GET: /Account/Register

    Public Function Register() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/Register

    <HttpPost()> _
    Public Function Register(ByVal model As RegisterModel) As ActionResult
        If ModelState.IsValid Then
            ' Attempt to register the user
            Dim createStatus As MembershipCreateStatus
            Membership.CreateUser(model.UserName, model.Password, model.Email, Nothing, Nothing, True, Nothing, createStatus)

            If createStatus = MembershipCreateStatus.Success Then
                FormsAuthentication.SetAuthCookie(model.UserName, False)
                Return RedirectToAction("Index", "Home")
            Else
                ModelState.AddModelError("", ErrorCodeToString(createStatus))
            End If
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    '
    ' GET: /Account/ChangePassword

    <Authorize()> _
    Public Function ChangePassword() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/ChangePassword

    <Authorize()> _
    <HttpPost()> _
    Public Function ChangePassword(ByVal model As ChangePasswordModel) As ActionResult
        If ModelState.IsValid Then
            ' ChangePassword will throw an exception rather
            ' than return false in certain failure scenarios.
            Dim changePasswordSucceeded As Boolean

            Try
                Dim currentUser As MembershipUser = Membership.GetUser(User.Identity.Name, True)
                changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword)
            Catch ex As Exception
                changePasswordSucceeded = False
            End Try

            If changePasswordSucceeded Then
                Return RedirectToAction("ChangePasswordSuccess")
            Else
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.")
            End If
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    '
    ' GET: /Account/ChangePasswordSuccess

    Public Function ChangePasswordSuccess() As ActionResult
        Return View()
    End Function

    Public Function Forgot() As ActionResult
        Return View()
    End Function

    <HttpPost()> _
    Public Function Forgot(email As String) As ActionResult
        Dim useremail As UserEmail = (From e In container.Emails Where e.Email = email).FirstOrDefault

        If useremail IsNot Nothing Then
            useremail.ConfirmationToken = Guid.NewGuid.ToString("N")
            container.SaveChanges()

            Dim mailer As New GetItDone.Mailers.EmailMailer
            mailer.Reset(useremail.Email, useremail.ConfirmationToken).Send()
        End If

        ViewBag.Message = "An email with instructions to reset the password was sent to " & email & "."
        Return View()
    End Function

    Public Function ResetPassword(id As String) As ActionResult
        Dim useremail As UserEmail = (From e In container.Emails Where e.ConfirmationToken = id).FirstOrDefault

        If useremail IsNot Nothing Then
            Dim mailer As New GetItDone.Mailers.EmailMailer
            Dim foundUser As MembershipUser = Membership.GetUser(useremail.OwnerId)
            mailer.NewPassword(useremail.Email, foundUser.UserName, foundUser.ResetPassword()).Send()
        End If

        Return View()
    End Function

#Region "Status Code"
    Public Function ErrorCodeToString(ByVal createStatus As MembershipCreateStatus) As String
        ' See http://go.microsoft.com/fwlink/?LinkID=177550 for
        ' a full list of status codes.
        Select Case createStatus
            Case MembershipCreateStatus.DuplicateUserName
                Return "User name already exists. Please enter a different user name."

            Case MembershipCreateStatus.DuplicateEmail
                Return "A user name for that e-mail address already exists. Please enter a different e-mail address."

            Case MembershipCreateStatus.InvalidPassword
                Return "The password provided is invalid. Please enter a valid password value."

            Case MembershipCreateStatus.InvalidEmail
                Return "The e-mail address provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.InvalidAnswer
                Return "The password retrieval answer provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.InvalidQuestion
                Return "The password retrieval question provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.InvalidUserName
                Return "The user name provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.ProviderError
                Return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator."

            Case MembershipCreateStatus.UserRejected
                Return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator."

            Case Else
                Return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator."
        End Select
    End Function
#End Region

End Class
