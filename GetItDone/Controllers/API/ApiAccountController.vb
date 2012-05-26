Namespace GetItDone.API
    Public Class ApiAccountController
        Inherits System.Web.Mvc.Controller


        <HttpPost()> _
        Public Function LogOn(ByVal model As LogOnModel) As JsonResult
            If ModelState.IsValid Then
                If Membership.ValidateUser(model.UserName, model.Password) Then
                    Dim resp As JsonResult = Json(Membership.GetUser(model.UserName).ProviderUserKey)
                    Return resp
                End If
            End If

            Return Json(False)
        End Function
    End Class
End Namespace
