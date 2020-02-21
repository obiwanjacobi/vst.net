namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Indication on the pan algorithm to use.
    /// </summary>
    public enum VstPanLaw
    {
        /// <summary>L = pan * M; R = (1 - pan) * M;</summary>
        LinearPanLaw = 0,
        /// <summary>L = pow (pan, 0.5) * M; R = pow ((1 - pan), 0.5) * M;</summary>
        EqualPowerPanLaw
    }
}
