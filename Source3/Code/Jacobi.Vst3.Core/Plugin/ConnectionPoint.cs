using Jacobi.Vst3.Common;
using Jacobi.Vst3.Core;
using System;

namespace Jacobi.Vst3.Plugin
{
    public abstract class ConnectionPoint : ObservableObject, IPluginBase, IConnectionPoint, IServiceContainerSite
    {
        private IConnectionPoint _peer;

        protected ConnectionPoint()
        {
            ServiceContainer = new ServiceContainer();
        }

        #region IServiceContainerSite Members

        public ServiceContainer ServiceContainer { get; protected set; }

        #endregion

        #region IPluginBase Members

        public virtual int Initialize(object context)
        {
            System.Diagnostics.Trace.WriteLine("IPluginBase.Initialize");

            ServiceContainer.Unknown = context;

            return TResult.S_OK;
        }

        public virtual int Terminate()
        {
            System.Diagnostics.Trace.WriteLine("IPluginBase.Terminate");

            _peer = null;

            ServiceContainer.Dispose();

            return TResult.S_OK;
        }

        #endregion

        #region IConnectionPoint Members

        public virtual int Connect(IConnectionPoint other)
        {
            System.Diagnostics.Trace.WriteLine("IConnectionPoint.Connect");

            if (other == null)
            {
                return TResult.E_InvalidArg;
            }
            if (_peer != null)
            {
                return TResult.S_False;
            }

            _peer = other;

            return TResult.S_OK;
        }

        public virtual int Disconnect(IConnectionPoint other)
        {
            System.Diagnostics.Trace.WriteLine("IConnectionPoint.Disconnect");

            if (_peer != null && _peer == other)
            {
                _peer = null;

                return TResult.S_OK;
            }

            return TResult.S_False;
        }

        public virtual int Notify(IMessage message)
        {
            if (message == null)
            {
                return TResult.E_InvalidArg;
            }

            System.Diagnostics.Trace.WriteLine("IConnectionPoint.Notify " + message.GetMessageID());

            return OnMessageReceived(new MessageEventArgs(message)) ? TResult.S_OK : TResult.S_False;
        }

        #endregion

        // use funcBeforeSend to populate message.
        protected virtual bool SendMessage(Action<IMessage> funcBeforeSend)
        {
            Guard.ThrowIfNull("funcBeforeSend", funcBeforeSend);

            if (_peer == null) return false;

            var host = ServiceContainer.GetService<IHostApplication>();
            if (host == null) return false;
            var msg = host.CreateMessage();

            if (msg != null)
            {
                funcBeforeSend(msg);

                var result = _peer.Notify(msg);

                return TResult.Succeeded(result);
            }

            return false;
        }

        protected virtual bool OnMessageReceived(MessageEventArgs messageEventArgs)
        {
            var handler = MessageReceived;

            if (handler != null)
            {
                handler(this, messageEventArgs);

                return true;
            }

            return false;
        }

        public event EventHandler<MessageEventArgs> MessageReceived;
    }
}
