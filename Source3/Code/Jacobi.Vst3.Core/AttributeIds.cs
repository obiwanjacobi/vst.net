namespace Jacobi.Vst3.Core
{
    public static class AttributeIds
    {
        public const string PlugInName = "PlugInName";             // Plug-in name
        public const string PlugInCategory = "PlugInCategory";     // eg. "Fx|Dynamics", "Instrument", "Instrument|Synth"
        public const string Instrument = "MusicalInstrument";      // eg. instrument group (like 'Piano' or 'Piano|A. Piano')
        public const string Style = "MusicalStyle";                // eg. 'Pop', 'Jazz', 'Classic'
        public const string Character = "MusicalCharacter";        // eg. instrument nature (like 'Soft' 'Dry' 'Acoustic')
        public const string StateType = "StateType";               // Type of the given state see \ref StateType : Project / Default Preset or Normal Preset
        public const string FilePathStringType = "FilePathString"; // Full file path string (if available) where the preset comes from (be sure to use a bigger string when asking for it (with 1024 characters))
        public const string Name = "Name";                         // name of the preset
        public const string FileName = "FileName";			       // filename of the preset (including extension)
    }
}
