using UnityEngine;
using System;
using System.Collections.Generic;

namespace Pool
{
    /// <summary>
    /// InfoPool can be used for exchanging information at a defined moment. When using InfoPool, the request for an information is initiated by the Receiver (pulling). 
    /// For each event, there is only one Provider allowed. The Provider defines the function that is called, when a Receiver requests the information.
    /// </summary>
    public class InfoPool : MonoBehaviour
    {
        private Dictionary<string, Delegate> dictionary;

        private static InfoPool infopool;

        private static InfoPool Instance
        {
            get
            {
                if (!infopool)
                {
                    infopool = FindObjectOfType(typeof(InfoPool)) as InfoPool;

                    if (!infopool)
                    {
                        GameObject newInfopool = new GameObject();
                        infopool = newInfopool.AddComponent<InfoPool>();
                        infopool.Init();
                    }
                    else
                    {
                        infopool.Init();
                    }
                }

                return infopool;
            }
        }

        void Init()
        {
            dictionary = new Dictionary<string, Delegate>();
        }

        /// <summary>
        /// Register a function that provides information. The information can be requested from anywhere using the <see cref="Request{T}(string)"/> function
        /// </summary>
        /// <typeparam name="T">The return value's type</typeparam>
        /// <param name="name">The event name</param>
        /// <param name="function">The callback function which will return the requested information</param>
        public static void Provide<T>(string name, Func<T> function)
        {
            Instance.dictionary.Remove(name);
            Instance.dictionary.Add(name, function);
        }

        /// <summary>
        /// Unregister a function that provides information
        /// </summary>
        public static void Unprovide<T>(string name, Func<T> function)
        {
            Delegate deleg = null;
            if (Instance.dictionary.TryGetValue(name, out deleg))
            {
                if (deleg.Equals(function))
                {
                    Instance.dictionary.Remove(name);
                }
            }
        }

        /// <summary>
        /// Request information defined by the event name. Prior to calling this function, a provider has to register using the <see cref="Provide{T}(string, Func{T})"/> function.
        /// </summary>
        public static T Request<T>(string name)
        {
            Delegate deleg = null;
            if (Instance.dictionary.TryGetValue(name, out deleg))
            {
                return (T)deleg.DynamicInvoke();
            }
            else
            {
                throw new RequestedItemNotProvidedException();
            }
        }

        public class RequestedItemNotProvidedException : Exception
        {
            public RequestedItemNotProvidedException() : base("The item with the given name has not been provided by anyone") { }
        }
    }
}