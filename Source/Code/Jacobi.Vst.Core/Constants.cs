namespace Jacobi.Vst.Core
{
    public class Constants
    {
        // VST 1.0 constants
        public const int MaxProgramNameLength = 23;	    // used for #effGetProgramName; #effSetProgramName; #effGetProgramNameIndexed
        public const int MaxParameterStringLength = 7;  // used for #effGetParamLabel; #effGetParamDisplay; #effGetParamName
        public const int MaxVendorStringLength = 63;	// used for #effGetVendorString; #audioMasterGetVendorString
        public const int MaxProductStringLength = 63;	// used for #effGetProductString; #audioMasterGetProductString
        public const int MaxEffectNameLength = 31;	    // used for #effGetEffectName

        // VST 2.0 constants
        public const int MaxMidiNameLength = 63;        // used for #MidiProgramName; #MidiProgramCategory; #MidiKeyName; #VstSpeakerProperties; #VstPinProperties
        public const int MaxLabelLength = 63;	        // used for #VstParameterProperties->label; #VstPinProperties->label
        public const int MaxShortLabelLength = 7;	    // used for #VstParameterProperties->shortLabel; #VstPinProperties->shortLabel
        public const int MaxCategoryLabelLength = 23;	// used for #VstParameterProperties->label
        public const int MaxFileNameLength = 99;	    // used for #VstAudioFile->name
    }
}
