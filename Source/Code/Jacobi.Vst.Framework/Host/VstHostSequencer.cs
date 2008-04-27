namespace Jacobi.Vst.Framework.Host
{
    using Jacobi.Vst.Core;

    internal class VstHostSequencer : IVstHostSequencer
    {
        private VstHost _host;

        public VstHostSequencer(VstHost host)
        {
            _host = host;
        }

        #region IVstHostSequencer Members

        public VstTimeInfo GetTime(VstTimeInfoFlags filterFlags)
        {
            return _host.HostCommandStub.GetTimeInfo(filterFlags);
        }

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

        #endregion

        #region IExtensibleObject Members

        public bool Supports<T>(bool threadSafe) where T : class
        {
            return _host.Supports<T>(threadSafe);
        }

        public T GetInstance<T>(bool threadSafe) where T : class
        {
            return _host.GetInstance<T>(threadSafe);
        }

        #endregion
    }
}
