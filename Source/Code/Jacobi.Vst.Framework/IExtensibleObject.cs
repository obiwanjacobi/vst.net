namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// Allows a client to query for an interface (typically) that is dynamically implemented by the object.
    /// </summary>
    public interface IExtensibleObject
    {
        /// <summary>
        /// Indicates if the interface <typeparamref name="T"/> is supported by the object.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <param name="threadSafe">True when the call is made from a different Thread than the object was created on.</param>
        /// <returns>Returns true if the interface <typeparamref name="T"/> is supported.</returns>
        bool Supports<T>(bool threadSafe) where T : class;

        /// <summary>
        /// Retrieves a reference to an implementation of the interface <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <param name="threadSafe">True when the call is made from a different Thread than the object was created on.</param>
        /// <returns>Returns null when the <typeparamref name="T"/> is not supported.</returns>
        T GetInstance<T>(bool threadSafe) where T : class;
    }
}
