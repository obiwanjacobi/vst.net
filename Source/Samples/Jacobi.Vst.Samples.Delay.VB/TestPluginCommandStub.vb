Imports Jacobi.Vst.Framework
Imports Jacobi.Vst.Framework.Plugin

Public Class TestPluginCommandStub
    Inherits StdPluginDeprecatedCommandStub
    Implements Core.Plugin.IVstPluginCommandStub

    Protected Overrides Function CreatePluginInstance() As Framework.IVstPlugin

        Return New FxTestPlugin()

    End Function

End Class
