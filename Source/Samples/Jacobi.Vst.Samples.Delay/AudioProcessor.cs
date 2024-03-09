using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using System.Diagnostics;

namespace Jacobi.Vst.Samples.Delay;

/// <summary>
/// This object performs audio processing for your plugin.
/// </summary>
internal sealed class AudioProcessor : VstPluginAudioProcessor, IVstPluginBypass
{
    /// <summary>Stereo inputs.</summary>
    private const int AudioInputCount = 2;
    /// <summary>Stereo outputs.</summary>
    private const int AudioOutputCount = 2;
    /// <summary>No tail size.</summary>
    private const int InitialTailSize = 0;

    private const VstTimeInfoFlags _defaultTimeInfoFlags = VstTimeInfoFlags.ClockValid;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public AudioProcessor(PluginParameters parameters)
        : base(AudioInputCount, AudioOutputCount, InitialTailSize, noSoundInStop: false)
    {
        Throw.IfArgumentIsNull(parameters, nameof(parameters));

        // one set of parameters is shared for both channels.
        Left = new Dsp.Delay(parameters.DelayParameters);
        Right = new Dsp.Delay(parameters.DelayParameters);
    }

    internal Dsp.Delay Left { get; private set; }
    internal Dsp.Delay Right { get; private set; }

    /// <summary>
    /// Override the default implementation to pass it through to the delay.
    /// </summary>
    public override float SampleRate
    {
        get { return Left.SampleRate; }
        set
        {
            Left.SampleRate = value;
            Right.SampleRate = value;
        }
    }

    /// <summary>
    /// Called by the host to allow the plugin to process audio samples.
    /// </summary>
    /// <param name="inChannels">Never null.</param>
    /// <param name="outChannels">Never null.</param>
    public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
    {
        if (!Bypass)
        {
            // check assumptions
            Debug.Assert(outChannels.Length == inChannels.Length);

            for (int i = 0; i < outChannels.Length; i++)
            {
                Process(i % 2 == 0 ? Left : Right,
                    inChannels[i], outChannels[i]);
            }
        }
        else
        {
            // calling the base class transfers input samples to the output channels unchanged (bypass).
            base.Process(inChannels, outChannels);
        }
    }

    // process a single audio channel
    private void Process(Dsp.Delay delay, VstAudioBuffer input, VstAudioBuffer output)
    {
        for (int i = 0; i < input.SampleCount; i++)
        {
            output[i] = delay.ProcessSample(input[i]);
        }
    }

    #region IVstPluginBypass Members

    public bool Bypass { get; set; }

    #endregion
}
