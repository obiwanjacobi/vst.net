namespace Jacobi.Vst.Framework
{
    public interface IActivatable
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}
