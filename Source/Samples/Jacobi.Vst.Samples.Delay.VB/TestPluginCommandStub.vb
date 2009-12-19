Imports Jacobi.Vst.Framework
Imports Jacobi.Vst.Framework.Plugin

Namespace Jacobi.Vst.Samples.Delay.VB

    Public Class TestPluginCommandStub
        Inherits StdPluginDeprecatedCommandStub
        Implements Core.Plugin.IVstPluginCommandStub

        Protected Overrides Function CreatePluginInstance() As Framework.IVstPlugin

            Return New FxTestPlugin()

        End Function

    End Class

End Namespace
