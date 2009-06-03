Imports Jacobi.Vst.Framework
Imports Jacobi.Vst.Framework.Plugin

Namespace Jacobi.Vst.Samples.Delay.VB

    Friend Class PluginPrograms
        Inherits VstPluginProgramsBase

        Private _plugin As FxTestPlugin

        Public Sub New(ByVal plugin As FxTestPlugin)
            _plugin = plugin
        End Sub

        Protected Overrides Function CreateProgramCollection() As Framework.VstProgramCollection
            Dim programs As VstProgramCollection = New VstProgramCollection()

            Dim prog As VstProgram = New VstProgram(_plugin.ParameterFactory.Categories)
            prog.Name = "Fx Program 1"
            _plugin.ParameterFactory.CreateParameters(prog.Parameters)

            programs.Add(prog)

            prog = New VstProgram(_plugin.ParameterFactory.Categories)
            prog.Name = "Fx Program 2"
            _plugin.ParameterFactory.CreateParameters(prog.Parameters)

            programs.Add(prog)

            prog = New VstProgram(_plugin.ParameterFactory.Categories)
            prog.Name = "Fx Program 3"
            _plugin.ParameterFactory.CreateParameters(prog.Parameters)

            programs.Add(prog)

            Return programs
        End Function

    End Class

End Namespace
