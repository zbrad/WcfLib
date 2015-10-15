using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ZBrad.WcfLib
{
    public sealed class Headers : IEndpointBehavior, IClientMessageInspector
    {
        List<MessageHeader> headers;

        public static void Add(ServiceEndpoint endpoint, IEnumerable<KeyValuePair<string,string>> pairs)
        {
            var h = new Headers();
            h.headers = new List<MessageHeader>();
            foreach (var kv in pairs)
                h.headers.Add(MessageHeader.CreateHeader(kv.Key, string.Empty, kv.Value));
            endpoint.Behaviors.Add(h);
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(this);
        }

        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            foreach (var h in headers)
                request.Headers.Add(h);
            return null;
        }

        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}
