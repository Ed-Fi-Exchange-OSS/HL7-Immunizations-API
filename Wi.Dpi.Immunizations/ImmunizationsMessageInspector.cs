using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Wi.Dpi.Immunizations
{
    public class ImmunizationsMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel aChannel)
        {
            request = new ImmunizationsMessage(request);
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public class ImmunizationsMessage : Message
        {
            private readonly Message _message;

            public ImmunizationsMessage(Message message)
            {
                _message = message;
            }

            protected override void OnWriteBodyContents(System.Xml.XmlDictionaryWriter writer)
            {
                var dynMethod = _message.GetType()
                    .GetMethod("OnWriteBodyContents", BindingFlags.NonPublic | BindingFlags.Instance);
                if (dynMethod != null) dynMethod.Invoke(_message, new object[] {writer});
            }

            public override MessageHeaders Headers
            {
                get
                {
                    // Remove wsa:Action header
                    int index = _message.Headers.FindHeader("Action", "http://www.w3.org/2005/08/addressing");
                    if (index > -1)
                    {
                        _message.Headers.RemoveAt(index);
                    }

                    // Remove wsa:To header
                    index = _message.Headers.FindHeader("To", "http://www.w3.org/2005/08/addressing");
                    if (index > -1)
                    {
                        _message.Headers.RemoveAt(index);
                    }

                    // Remove wsa:ReplyTo header
                    index = _message.Headers.FindHeader("ReplyTo", "http://www.w3.org/2005/08/addressing");
                    if (index > -1)
                    {
                        _message.Headers.RemoveAt(index);
                    }

                    // Remove VsDebuggerCausalityData (only appears in Dev but here from convenience)
                    index = _message.Headers.FindHeader("VsDebuggerCausalityData",
                        "http://schemas.microsoft.com/vstudio/diagnostics/servicemodelsink");
                    if (index > -1)
                    {
                        _message.Headers.RemoveAt(index);
                    }

                    return _message.Headers;
                }
            }

            public override MessageProperties Properties => _message.Properties;

            public override MessageVersion Version => _message.Version;
        }
    }
}
