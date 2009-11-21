Imports Jacobi.Vst.Core
Imports Jacobi.Vst.Framework
Imports Jacobi.Vst.Framework.Plugin

Namespace Jacobi.Vst.Samples.Delay.VB

    Friend Class AudioProcessor
        Inherits VstPluginAudioProcessorBase

        Private _plugin As FxTestPlugin
        Private _delay As Delay

        Public Sub New(ByVal plugin As FxTestPlugin)
            MyBase.New(1, 1, 0)

            _plugin = plugin
            _delay = New Delay()

        End Sub

        Public ReadOnly Property Delay() As Delay
            Get
                Return _delay
            End Get
        End Property

        Public Overrides Property SampleRate() As Single
            Get
                Return _delay.SampleRate
            End Get
            Set(ByVal value As Single)
                _delay.SampleRate = value
            End Set
        End Property

        Public Overrides Sub Process(ByVal inChannels() As Core.VstAudioBuffer, ByVal outChannels() As Core.VstAudioBuffer)
            Dim AudioChannel As VstAudioBuffer = outChannels(0)


            For n As Integer = 0 To AudioChannel.SampleCount - 1

                AudioChannel(n) = Delay.ProcessSample(inChannels(0)(n))

            Next

        End Sub
    End Class

End Namespace
