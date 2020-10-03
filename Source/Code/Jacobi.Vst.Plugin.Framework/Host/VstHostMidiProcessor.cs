namespace Jacobi.Vst.Plugin.Framework.Host
{
    using Jacobi.Vst.Core;
    using System;
    using System.Linq;

    /// <summary>
    /// Forwards the <see cref="IVstMidiProcessor"/> calls to the host stub.
    /// </summary>
    internal sealed class VstHostMidiProcessor : IVstMidiProcessor
    {
        private readonly IVstHostCommands20 _commands;

        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="commands">Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="commands"/> is not set to an instance of an object.</exception>
        public VstHostMidiProcessor(IVstHostCommands20 commands)
        {
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        #region IVstMidiProcessor Members

        /// <summary>
        /// Always returns 16.
        /// </summary>
        public int ChannelCount
        {
            // default number of channels for host
            get { return 16; }
        }

        /// <summary>
        /// Passes the <paramref name="events"/> onto the host.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="events"/> is not set to an instance of an object.</exception>
        public void Process(VstEventCollection events)
        {
            Throw.IfArgumentIsNull(events, nameof(events));

            _commands.ProcessEvents(events.ToArray());
        }

        #endregion
    }
}
