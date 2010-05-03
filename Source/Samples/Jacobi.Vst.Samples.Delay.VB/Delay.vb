Imports System
Imports System.ComponentModel
Imports Jacobi.Vst.Framework

Namespace Jacobi.Vst.Samples.Delay.VB

    Friend Class Delay

        Private _delayBuffer As Single()
        Private _bufferIndex As Integer
        Private _bufferLength As Integer

        Private WithEvents _delayTimeMgr As VstParameterManager
        Private _feedbackMgr As VstParameterManager
        Private _dryLevelMgr As VstParameterManager
        Private _wetLevelMgr As VstParameterManager

        Public Sub New()
            _paramInfos = New VstParameterInfoCollection()

            ' delay time parameter
            Dim paramInfo As VstParameterInfo = New VstParameterInfo()
            paramInfo.CanBeAutomated = True
            paramInfo.Name = "dt"
            paramInfo.Label = "Delay Time"
            paramInfo.ShortLabel = "T-Dly:"
            paramInfo.MinInteger = 0
            paramInfo.MaxInteger = 1000
            paramInfo.LargeStepFloat = 100.0F
            paramInfo.SmallStepFloat = 1.0F
            paramInfo.StepFloat = 10.0F
            paramInfo.DefaultValue = 200.0F
            _delayTimeMgr = New VstParameterManager(paramInfo)
            VstParameterNormalizationInfo.AttachTo(paramInfo)

            _paramInfos.Add(paramInfo)

            ' feedback parameter
            paramInfo = New VstParameterInfo()
            paramInfo.CanBeAutomated = True
            paramInfo.Name = "fb"
            paramInfo.Label = "Feedback"
            paramInfo.ShortLabel = "Feedbk:"
            paramInfo.LargeStepFloat = 0.1F
            paramInfo.SmallStepFloat = 0.01F
            paramInfo.StepFloat = 0.05F
            paramInfo.DefaultValue = 0.2F
            _feedbackMgr = New VstParameterManager(paramInfo)
            VstParameterNormalizationInfo.AttachTo(paramInfo)

            _paramInfos.Add(paramInfo)

            ' dry Level parameter
            paramInfo = New VstParameterInfo()
            paramInfo.CanBeAutomated = True
            paramInfo.Name = "dl"
            paramInfo.Label = "Dry Level"
            paramInfo.ShortLabel = "DryLvl:"
            paramInfo.LargeStepFloat = 0.1F
            paramInfo.SmallStepFloat = 0.01F
            paramInfo.StepFloat = 0.05F
            paramInfo.DefaultValue = 0.8F
            _dryLevelMgr = New VstParameterManager(paramInfo)
            VstParameterNormalizationInfo.AttachTo(paramInfo)

            _paramInfos.Add(paramInfo)

            ' wet Level parameter
            paramInfo = New VstParameterInfo()
            paramInfo.CanBeAutomated = True
            paramInfo.Name = "wl"
            paramInfo.Label = "Wet Level"
            paramInfo.ShortLabel = "WetLvl:"
            paramInfo.LargeStepFloat = 0.1F
            paramInfo.SmallStepFloat = 0.01F
            paramInfo.StepFloat = 0.05F
            paramInfo.DefaultValue = 0.4F
            _wetLevelMgr = New VstParameterManager(paramInfo)
            VstParameterNormalizationInfo.AttachTo(paramInfo)

            _paramInfos.Add(paramInfo)

        End Sub

        Private Sub _delayTimeMgr_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Handles _delayTimeMgr.PropertyChanged

            If e.PropertyName = "CurrentValue" Then
                Dim paramMgr As VstParameterManager = CType(sender, VstParameterManager)
                _bufferLength = CType(paramMgr.CurrentValue * _sampleRate / 1000, Integer)
            End If
        End Sub

        'Private Sub _delayTimeMgr_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _delayTimeMgr.ValueChanged
        '    Dim paramMgr As VstParameterManager = CType(sender, VstParameterManager)
        '    _bufferLength = CType(paramMgr.CurrentValue * _sampleRate / 1000, Integer)
        'End Sub

        Private _paramInfos As VstParameterInfoCollection
        Public ReadOnly Property ParameterInfos() As VstParameterInfoCollection
            Get
                Return _paramInfos
            End Get
        End Property

        Private _sampleRate As Single
        Public Property SampleRate() As Single
            Get
                Return _sampleRate
            End Get
            Set(ByVal value As Single)
                _sampleRate = value

                ' allocate buffer for max delay time
                Dim bufferLength As Int32 = CType(_delayTimeMgr.ParameterInfo.MaxInteger * _sampleRate / 1000, Integer)
                _delayBuffer = New Single(bufferLength) {}

                _bufferLength = CType(_delayTimeMgr.CurrentValue * _sampleRate / 1000, Integer)
            End Set
        End Property

        Public Function ProcessSample(ByVal sample As Single) As Single
            If _delayBuffer Is Nothing Then Return sample

            ' process output
            Dim output As Single = (_dryLevelMgr.CurrentValue * sample) + (_wetLevelMgr.CurrentValue * _delayBuffer(_bufferIndex))

            ' process delay buffer
            _delayBuffer(_bufferIndex) = sample + (_feedbackMgr.CurrentValue * _delayBuffer(_bufferIndex))

            _bufferIndex = _bufferIndex + 1

            ' manage current buffer position
            If _bufferIndex >= _bufferLength Then _bufferIndex = 0

            Return output
        End Function

    End Class

End Namespace
