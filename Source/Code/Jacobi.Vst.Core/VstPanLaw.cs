namespace Jacobi.Vst.Core
{
    public enum VstPanLaw
    {
        LinearPanLaw = 0,	// L = pan * M; R = (1 - pan) * M;
        EqualPowerPanLaw	// L = pow (pan, 0.5) * M; R = pow ((1 - pan), 0.5) * M;
    }
}
