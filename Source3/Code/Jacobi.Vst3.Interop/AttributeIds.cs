namespace Jacobi.Vst3.Interop
{
    public static class AttributeIds
    {
        public const string PlugInName = "PlugInName";		///< Plug-in name
        public const string PlugInCategory = "PlugInCategory";	///< eg. "Fx|Dynamics", "Instrument", "Instrument|Synth"

        public const string Instrument = "MusicalInstrument";	///< eg. instrument group (like 'Piano' or 'Piano|A. Piano')
        public const string Style = "MusicalStyle";		///< eg. 'Pop', 'Jazz', 'Classic'
        public const string Character = "MusicalCharacter";	///< eg. instrument nature (like 'Soft' 'Dry' 'Acoustic')	
    }
}
