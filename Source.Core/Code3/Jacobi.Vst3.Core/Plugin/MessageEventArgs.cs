using Jacobi.Vst3.Core;
using System;

namespace Jacobi.Vst3.Plugin
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(IMessage message)
        {
            Message = message;
        }

        public IMessage Message { get; private set; }
    }
}
