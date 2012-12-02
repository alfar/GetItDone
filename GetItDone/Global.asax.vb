Imports StackExchange.Profiling
' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
        filters.Add(New HandleErrorAttribute())
    End Sub

    Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")


        ' MapRoute takes the following parameters, in order:
        ' (1) Route name
        ' (2) URL with parameters
        ' (3) Parameter defaults

        routes.MapRoute("loginAPI", "api/account/{action}", New With {.controller = "ApiAccount"}, Nothing, New String() {"GetItDone.GetItDone.API"})

        routes.MapRoute( _
            "Default", _
            "{controller}/{action}/{id}", _
            New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}, _
            Nothing,
            New String() {"GetItDone", "GetItDone.GetItDone"}
        )

        routes.MapRoute("API", "api/{userkey}/{controller}/{action}/{id}", New With {.action = "Index", .id = UrlParameter.Optional}, Nothing, New String() {"GetItDone.GetItDone.API"})
    End Sub

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        RegisterGlobalFilters(GlobalFilters.Filters)
        RegisterRoutes(RouteTable.Routes)
    End Sub


    Private Sub MvcApplication_BeginRequest(sender As Object, e As System.EventArgs) Handles Me.BeginRequest
        If Not Request.Path.ToLower().Contains("/api/") Then
            MiniProfiler.Start()
        End If
    End Sub

    Private Sub MvcApplication_EndRequest(sender As Object, e As System.EventArgs) Handles Me.EndRequest
        MiniProfiler.Stop()
    End Sub
End Class
