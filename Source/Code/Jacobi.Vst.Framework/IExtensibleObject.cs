namespace Jacobi.Vst.Framework
{
    public interface IExtensibleObject
    {
        bool Supports<T>(bool threadSafe) where T : class;
        T GetInstance<T>(bool threadSafe) where T : class;
    }
}
