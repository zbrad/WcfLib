using System;
using Sm = System.ServiceModel;
using Smc = System.ServiceModel.Channels;
using Smd = System.ServiceModel.Description;
using System.Collections.Generic;

namespace ZBrad.WcfLib
{
    public abstract class ClientBase<T>
        where T : class
    {
        Uri path;
        Uri via;
        Smc.Binding binding;
        static Smd.ContractDescription contract = Smd.ContractDescription.GetContract(typeof(T));

        protected List<Smd.IEndpointBehavior> Behaviors = new List<Smd.IEndpointBehavior>();

        internal ClientBase(Smc.Binding b)
        {
            binding = b;
        }

        /// <summary>
        /// the current instance
        /// </summary>
        public T Instance { get; private set; }

        internal bool TryInit(Uri path, KeyValuePair<string, string>[] headers)
        {
            try
            {
                initClient(path, headers);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool TryInit(Uri path, Uri via, KeyValuePair<string, string>[] headers)
        {
            try
            {
                initClient(path, via, headers);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region private methods 

        void initFactory(Uri path, KeyValuePair<string, string>[] headers)
        {
            this.path = Util.GetWcfUri(path);
            var address = new Sm.EndpointAddress(this.path);
            var endpoint = new Smd.ServiceEndpoint(contract, binding, address);

            if (headers != null)
                Headers.Add(endpoint, headers);

            foreach (var b in Behaviors)
                endpoint.Behaviors.Add(b);

            var factory = new Sm.ChannelFactory<T>(endpoint);
            this.Instance = factory.CreateChannel();
        }

        void initClient(Uri path, KeyValuePair<string, string>[] headers)
        {
            initFactory(path, headers);
        }

        void initClient(Uri path, Uri via, KeyValuePair<string, string>[] headers)
        {
            this.via = via;
            this.Behaviors.Add(new Smd.ClientViaBehavior(this.via));
            initFactory(path, headers);
        }
        #endregion
    }
}
