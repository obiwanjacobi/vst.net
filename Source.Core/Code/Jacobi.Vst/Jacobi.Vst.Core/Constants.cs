namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Constants used in the VST interface
    /// </summary>
    public static class Constants
    {
        // VST 1.0 constants
        /// <summary>used for #effGetProgramName; #effSetProgramName; #effGetProgramNameIndexed</summary>
        public const int MaxProgramNameLength = 24;
        /// <summary>used for #effGetParamLabel; #effGetParamDisplay; #effGetParamName</summary>
        public const int MaxParameterStringLength = 8;
        /// <summary>used for #effGetVendorString; #audioMasterGetVendorString</summary>
        public const int MaxVendorStringLength = 64;
        /// <summary>used for #effGetProductString; #audioMasterGetProductString</summary>
        public const int MaxProductStringLength = 64;
        /// <summary>used for #effGetEffectName</summary>
        public const int MaxEffectNameLength = 32;

        // VST 2.0 constants
        /// <summary>used for #MidiProgramName; #MidiProgramCategory; #MidiKeyName; #VstSpeakerProperties; #VstPinProperties</summary>
        public const int MaxMidiNameLength = 64;
        /// <summary>used for #VstParameterProperties->label; #VstPinProperties->label</summary>
        public const int MaxLabelLength = 64;
        /// <summary>used for #VstParameterProperties->shortLabel; #VstPinProperties->shortLabel</summary>
        public const int MaxShortLabelLength = 8;
        /// <summary>used for #VstParameterProperties->label</summary>
        public const int MaxCategoryLabelLength = 24;
        /// <summary>used for #VstAudioFile->name</summary>
        public const int MaxFileNameLength = 100;

        /// <summary>used for #VstFileSelect->title</summary>
        public const int MaxFileSelectorTitle = 1024;
        /// <summary>used for #VstFileType->name and mimeType(2)</summary>
        public const int MaxFileTypeName = 128;
        /// <summary>used for #VstFileType->dos/unix/macType</summary>
        public const int MaxFileTypeExtension = 8;

        /// <summary>used in both host and plugin cando's.</summary>
        public const int MaxCanDoLength = 64;
    }
}
