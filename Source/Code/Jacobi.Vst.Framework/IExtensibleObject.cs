namespace Jacobi.Vst.Framework
{
    public interface IExtensibleObject
    {
        bool Supports<T>(bool threadSafe);
        T GetInstance<T>(bool threadSafe);
    }
}
