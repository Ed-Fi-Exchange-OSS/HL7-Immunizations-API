using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Wi.Dpi.Immunizations
{
    public class ImmunizationsEndpointBehavior : IEndpointBehavior
    {
        private ImmunizationsMessageInspector _messageInspector;

        public ImmunizationsMessageInspector ClientMessageInspector => _messageInspector ?? (_messageInspector = new ImmunizationsMessageInspector());

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(ClientMessageInspector);
        }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }
        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
