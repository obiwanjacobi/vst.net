namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// Allows a client to query for an interface (typically) that is dynamically implemented by the object.
    /// </summary>
    public interface IExtensible
    {
        /// <summary>
        /// Indicates if the interface <typeparamref name="T"/> is supported by the object.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>Returns true if the interface <typeparamref name="T"/> is supported.</returns>
        bool Supports<T>() where T : class;

        /// <summary>
        /// Retrieves a reference to an implementation of the interface <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>Returns null when the <typeparamref name="T"/> is not supported.</returns>
        T GetInstance<T>() where T : class;
    }
}
