Imports Jacobi.Vst.Framework.Plugin

Friend Class FxPluginInterfaceManager
    Inherits PluginInterfaceManagerBase

    Private _plugin As FxTestPlugin

    Public Sub New(ByVal plugin As FxTestPlugin)
        _plugin = plugin
    End Sub

    Protected Overrides Function CreateAudioProcessor(ByVal instance As Framework.IVstPluginAudioProcessor) As Framework.IVstPluginAudioProcessor
        If instance Is Nothing Then
            Return New AudioProcessor(_plugin)
        End If

        Return instance
    End Function

    Protected Overrides Function CreatePrograms(ByVal instance As Framework.IVstPluginPrograms) As Framework.IVstPluginPrograms
        If instance Is Nothing Then
            Return New PluginPrograms(_plugin)
        End If

        Return instance
    End Function

    Protected Overrides Function CreatePersistence(ByVal instance As Framework.IVstPluginPersistence) As Framework.IVstPluginPersistence
        If instance Is Nothing Then
            Return New PluginPersistence(_plugin)
        End If

        Return instance
    End Function
End Class
