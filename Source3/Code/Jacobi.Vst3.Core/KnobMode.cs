namespace Jacobi.Vst3.Core
{
    public enum KnobModes
    {
        CircularMode = 0,		// Circular with jump to clicked position
        RelativCircularMode,	// Circular without jump to clicked position
        LinearMode				// Linear: depending on vertical movement
    };
}
