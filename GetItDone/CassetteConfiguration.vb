Imports Cassette.Configuration
Imports Cassette.Scripts
Imports Cassette.Stylesheets

namespace GetItDone
    ''' <summary>
    ''' Configures the Cassette asset modules for the web application.
    ''' </summary>
    Public Class CassetteConfiguration
        Implements ICassetteConfiguration

        Public Sub Configure(bundles As BundleCollection, settings As CassetteSettings) Implements ICassetteConfiguration.Configure
            ' TODO: Configure your bundles here...
            ' Please read http:'getcassette.net/documentation/configuration

            ' This default configuration treats each file as a separate 'bundle'.
            ' In production the content will be minified, but the files are not combined.
            ' So you probably want to tweak these defaults!
            bundles.Add(Of StylesheetBundle)("Content")
            bundles.AddPerSubDirectory(Of ScriptBundle)("Scripts")

            ' To combine files, try something like this instead:
            '   bundles.Add<StylesheetBundle>("Content");
            ' In production mode, all of ~/Content will be combined into a single bundle.

            ' If you want a bundle per folder, try this:
            '   bundles.AddPerSubDirectory<ScriptBundle>("Scripts");
            ' Each immediate sub-directory of ~/Scripts will be combined into its own bundle.
            ' This is useful when there are lots of scripts for different areas of the website.

            ' *** TOP TIP: Delete all ".min.js" files now ***
            ' Cassette minifies scripts for you. So those files are never used.
        End Sub
    End Class
End Namespace