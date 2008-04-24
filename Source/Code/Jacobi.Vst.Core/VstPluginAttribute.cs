namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Marker attribute for a plugin root class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false,Inherited=false)]
    public class VstPluginAttribute : Attribute
    {
    }
}
