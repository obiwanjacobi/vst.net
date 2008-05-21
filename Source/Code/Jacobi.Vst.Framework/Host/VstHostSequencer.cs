namespace Jacobi.Vst.Framework.Host
{
    using Jacobi.Vst.Core;

    internal class VstHostSequencer : IVstHostSequencer
    {
        private VstHost _host;

        public VstHostSequencer(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

            _host = host;
        }

        #region IVstHostSequencer Members

        public double SampleRate
        {
            get { return _host.HostCommandStub.GetSampleRate(); }
        }

        public int BlockSize
        {
            get { return _host.HostCommandStub.GetBlockSize(); }
        }

        public int InputLatency
        {
            get { return _host.HostCommandStub.GetInputLatency(); }
        }

        public int OutputLatency
        {
            get { return _host.HostCommandStub.GetOutputLatency(); }
        }

        public VstProcessLevels ProcessLevel
        {
            get { return _host.HostCommandStub.GetProcessLevel(); }
        }

        public VstTimeInfo GetTime(VstTimeInfoFlags filterFlags)
        {
            return _host.HostCommandStub.GetTimeInfo(filterFlags);
        }

        public bool UpdatePluginIO()
        {
            return _host.HostCommandStub.IoChanged();
        }

        #endregion

        #region IExtensibleObject Members

        public bool Supports<T>() where T : class
        {
            return _host.Supports<T>();
        }

        public T GetInstance<T>() where T : class
        {
            return _host.GetInstance<T>();
        }

        #endregion
    }
}
