Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports Jacobi.Vst.Core
Imports Jacobi.Vst.Framework
Imports Jacobi.Vst.Framework.Plugin
Imports Jacobi.Vst.Framework.Plugin.IO

Friend Class PluginPersistence
    Inherits VstPluginPersistenceBase

    Private _plugin As FxTestPlugin

    Public Sub New(ByVal plugin As FxTestPlugin)
        _plugin = plugin
    End Sub

    Protected Overrides Function CreateProgramReader(ByVal input As Stream) As VstProgramReaderBase
        Return New DelayProgramReader(_plugin, input, Encoding)
    End Function

    Private Class DelayProgramReader
        Inherits VstProgramReaderBase

        Private _plugin As FxTestPlugin

        Public Sub New(ByVal plugin As FxTestPlugin, ByVal input As Stream, ByVal encoding As Encoding)
            MyBase.New(input, encoding)

            _plugin = plugin
        End Sub

        Protected Overrides Function CreateProgram() As Framework.VstProgram
            Dim program As VstProgram = New VstProgram(_plugin.ParameterFactory.Categories)

            _plugin.ParameterFactory.CreateParameters(program.Parameters)

            Return program
        End Function

    End Class
End Class
