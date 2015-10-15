using System;
using Sm = System.ServiceModel;
using Smc = System.ServiceModel.Channels;
using Smd = System.ServiceModel.Description;
using System.Collections.Generic;

namespace ZBrad.WcfLib
{
    public class RestClient<T> : ClientBase<T>
        where T : class
    {
        static Sm.WebHttpBinding binding = new Sm.WebHttpBinding(Sm.WebHttpSecurityMode.None);
        static Smd.WebHttpBehavior behavior = new Smd.WebHttpBehavior();
        internal RestClient() : base(binding)
        {
            this.Behaviors.Add(behavior);
        }

        /// <summary>
        /// create a WCF TCP client
        /// </summary>
        /// <param name="path">endpoint name</param>
        /// <param name="client">the new client</param>
        /// <param name="headers">optional headers to add</param>
        /// <returns>true if client created</returns>
        public static bool TryCreate(Uri path, out RestClient<T> client, params KeyValuePair<string, string>[] headers)
        {
            client = new RestClient<T>();
            return client.TryInit(path, headers);
        }

        /// <summary>
        /// create a WCF TCP client
        /// </summary>
        /// <param name="path">endpoint name</param>
        /// <param name="via">endpoint of router to resolve name</param>
        /// <param name="client">the new client</param>
        /// <param name="headers">optional headers to add</param>
        /// <returns>true if client created</returns>
        public static bool TryCreate(Uri path, Uri via, out RestClient<T> client, params KeyValuePair<string, string>[] headers)
        {
            client = new RestClient<T>();
            return client.TryInit(path, via, headers);
        }
    }
}
