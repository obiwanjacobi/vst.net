Imports Jacobi.Vst.Core
Imports Jacobi.Vst.Framework
Imports Jacobi.Vst.Framework.Plugin

Friend Class FxTestPlugin
    Inherits VstPluginBase

    Private _intfMgr As FxPluginInterfaceManager

    Public Sub New()

        MyBase.New("VST.NET Delay Plugin", _
                New VstProductInfo("VST.NET Code Samples", "Jacobi Software (c) 2011", 1000), _
                VstPluginCategory.RoomFx, VstPluginCapabilities.None, 0, &H3A3A3A3A)

        _intfMgr = New FxPluginInterfaceManager(Me)
        _paramFactory = New PluginParameterFactory()

        Dim audioProc As AudioProcessor = _intfMgr.GetInstance(Of AudioProcessor)()
        ' add delay parameters to factory
        ParameterFactory.ParameterInfos.AddRange(audioProc.Delay.ParameterInfos)

    End Sub

    Private _paramFactory As PluginParameterFactory
    Public ReadOnly Property ParameterFactory() As PluginParameterFactory
        Get
            Return _paramFactory
        End Get
    End Property

    Public Overrides Function Supports(Of T As Class)() As Boolean
        Return _intfMgr.Supports(Of T)()
    End Function

    Public Overrides Function GetInstance(Of T As Class)() As T
        Return _intfMgr.GetInstance(Of T)()
    End Function

    Public Overrides Sub Dispose()
        _intfMgr.Dispose()
        _intfMgr = Nothing

        MyBase.Dispose()
    End Sub
End Class
