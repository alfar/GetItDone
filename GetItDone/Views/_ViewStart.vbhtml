@Code
    If Request.Browser.IsMobileDevice OrElse Request.Headers("User-Agent").ToLower().Contains("android") Then
        Layout = "~/Views/Shared/_Layout.Mobile.vbhtml"
    Else
        Layout = "~/Views/Shared/_Layout.vbhtml"        
    End If
End Code