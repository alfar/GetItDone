Imports StackExchange.Profiling

Public NotInheritable Class TaskModelFactory
    Private Sub New()
    End Sub

    Public Shared Function GetModel() As TaskModelContainer
        If MiniProfiler.Current IsNot Nothing Then
            Dim conn As New StackExchange.Profiling.Data.EFProfiledDbConnection(GetConnection(), MiniProfiler.Current)
            Return StackExchange.Profiling.Data.ObjectContextUtils.CreateObjectContext(Of TaskModelContainer)(conn)
        Else
            Return New TaskModelContainer()
        End If
    End Function

    Public Shared Function GetConnection() As System.Data.Common.DbConnection
        Dim ecb As New EntityClient.EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings("TaskModelContainer").ConnectionString)
        Return New SqlClient.SqlConnection(ecb.ProviderConnectionString)
    End Function
End Class
