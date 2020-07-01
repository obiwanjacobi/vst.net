using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jacobi.Vst3.Plugin
{
    public sealed class CategoryCollection : Collection<string>
    {
        public const string FxAnalyzer = "Fx|Analyzer";	// Scope, FFT-Display,...
        public const string FxDelay = "Fx|Delay";		// Delay, Multi-tap Delay, Ping-Pong Delay...
        public const string FxDistortion = "Fx|Distortion";	// Amp Simulator, Sub-Harmonic, SoftClipper...
        public const string FxDynamics = "Fx|Dynamics";	// Compressor, Expander, Gate, Limiter, Maximizer, Tape Simulator, EnvelopeShaper...
        public const string FxEQ = "Fx|EQ";			// Equalization, Graphical EQ...
        public const string FxFilter = "Fx|Filter";		// WahWah, ToneBooster, Specific Filter,...
        public const string Fx = "Fx";				// others type (not categorized)
        public const string FxInstrument = "Fx|Instrument";	// Fx which could be loaded as Instrument too
        public const string FxInstrumentExternal = "Fx|Instrument|External";	// Fx which could be loaded as Instrument too and is external (wrapped Hardware)
        public const string FxSpatial = "Fx|Spatial";		// MonoToStereo, StereoEnhancer,...
        public const string FxGenerator = "Fx|Generator";	// Tone Generator, Noise Generator...
        public const string FxMastering = "Fx|Mastering";	// Dither, Noise Shaping,...
        public const string FxModulation = "Fx|Modulation";	// Phaser, Flanger, Chorus, Tremolo, Vibrato, AutoPan, Rotary, Cloner...
        public const string FxPitchShift = "Fx|Pitch Shift";	// Pitch Processing, Pitch Correction,...
        public const string FxRestoration = "Fx|Restoration";	// Denoiser, Declicker,...
        public const string FxReverb = "Fx|Reverb";		// Reverberation, Room Simulation, Convolution Reverb...
        public const string FxSurround = "Fx|Surround";	// dedicated to surround processing: LFE Splitter, Bass Manager...
        public const string FxTools = "Fx|Tools";		// Volume, Mixer, Tuner...

        public const string Instrument = "Instrument";			// Effect used as instrument (sound generator), not as insert
        public const string InstrumentDrum = "Instrument|Drum";	// Instrument for Drum sounds
        public const string InstrumentSampler = "Instrument|Sampler";	// Instrument based on Samples
        public const string InstrumentSynth = "Instrument|Synth";	// Instrument based on Synthesis
        public const string InstrumentSynthSampler = "Instrument|Synth|Sampler";	// Instrument based on Synthesis and Samples
        public const string InstrumentExternal = "Instrument|External";// External Instrument (wrapped Hardware)

        public const string Spatial = "Spatial";		// used for SurroundPanner
        public const string SpatialFx = "Spatial|Fx";		// used for SurroundPanner and as insert effect
        public const string OnlyRealTime = "OnlyRT";			// indicates that it supports only realtime process call, no processing faster than realtime
        public const string OnlyOfflineProcess = "OnlyOfflineProcess";	// used for offline processing Plug-in (will not work as normal insert Plug-in)
        public const string UpDownMix = "Up-Downmix";		// used for Mixconverter/Up-Mixer/Down-Mixer
        public const string Analyzer = "Analyzer";	    // Meter, Scope, FFT-Display, not selectable as insert plugin

        public const string Mono = "Mono";			// used for Mono only Plug-in [optional]
        public const string Stereo = "Stereo";			// used for Stereo only Plug-in [optional]
        public const string Surround = "Surround";		// used for Surround only Plug-in [optional]

        public CategoryCollection()
        { }

        public CategoryCollection(string parse)
        {
            var cats = parse.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cat in cats)
            {
                Add(cat);
            }
        }

        public override string ToString()
        {
            return String.Join("|", this.ToArray());
        }
    }
}
