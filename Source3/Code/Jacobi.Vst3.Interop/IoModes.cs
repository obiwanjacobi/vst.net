namespace Jacobi.Vst3.Interop
{
    public enum IoModes
    {
        Simple = 0,		///< 1:1 Input / Output. Only used for Instruments. See \ref vst3IoMode
        Advanced,			///< n:m Input / Output. Only used for Instruments. 
        OfflineProcessing	///< Plug-in used in an offline processing context
    }
}
