namespace Jacobi.Vst3.Interop
{
    public enum KnobModes
    {
        CircularMode = 0,		///< Circular with jump to clicked position
        RelativCircularMode,	///< Circular without jump to clicked position
        LinearMode				///< Linear: depending on vertical movement
    };
}
