using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using _GameAssets.Scripts;

namespace Pool
{
    /// <summary>
    /// EventPool manages your events. You can StartListening, OutOut and Trigger Events for decoupling your components
    /// </summary>
    public class EventPool : MonoBehaviour
    {
        private Dictionary<EventTypes, IConcreteUnityEvent> events0Arg, events1Arg, events2Arg, events3Arg;

        private static EventPool manager;

        private static EventPool Instance
        {
            get
            {
                if (!manager)
                {
                    manager = FindObjectOfType(typeof(EventPool)) as EventPool;

                    if (!manager)
                    {
                        GameObject newManager = new GameObject();
                        manager = newManager.AddComponent<EventPool>();
                        manager.Initialize();
                    }
                    else
                    {
                        manager.Initialize();
                    }
                }

                return manager;
            }
        }

        public static void Reset()
        {
            Instance.events0Arg = new Dictionary<EventTypes, IConcreteUnityEvent>();
            Instance.events1Arg = new Dictionary<EventTypes, IConcreteUnityEvent>();
            Instance.events2Arg = new Dictionary<EventTypes, IConcreteUnityEvent>();
            Instance.events3Arg = new Dictionary<EventTypes, IConcreteUnityEvent>();
        }

        private void Initialize()
        {
            Reset();
        }

        /// <summary>
        /// Register a listener for an event using no arguments.
        /// </summary>
        public static void StartListening(EventTypes eventName, UnityAction listener)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events0Arg.TryGetValue(eventName, out thisEvent))
            {
                if (!(thisEvent is ConcreteUnityEvent))
                {
                    Debug.LogError("This Key is already used for an event with a different signiture: " + eventName);
                    return;
                }

                Cast(thisEvent).RemoveListener(listener); // Make sure the same listener is never added more than once
                Cast(thisEvent).AddListener(listener);
            }
            else
            {
                thisEvent = new ConcreteUnityEvent();
                Cast(thisEvent).AddListener(listener);
                Instance.events0Arg.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// Remove a listener with no arguments
        /// </summary>
        public static void StopListening(EventTypes eventName, UnityAction listener)
        {
            if (manager == null) return;
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events0Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast(thisEvent).RemoveListener(listener);
            }
        }

        /// <summary>
        /// Trigger an event with no arguments
        /// </summary>
        public static void Trigger(EventTypes eventName)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events0Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast(thisEvent).Invoke();
            }
        }

        /// <summary>
        /// Register a listener for an event using 1 argument.
        /// </summary>
        public static void StartListening<T>(EventTypes eventName, UnityAction<T> listener)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events1Arg.TryGetValue(eventName, out thisEvent))
            {
                if (!(thisEvent is ConcreteUnityEvent<T>))
                {
                    Debug.LogError("This Key is already used for an event with a different signiture: " + eventName);
                    return;
                }

                Cast<T>(thisEvent).RemoveListener(listener); // Make sure the same listener is never added more than once
                Cast<T>(thisEvent).AddListener(listener);
            }
            else
            {
                thisEvent = new ConcreteUnityEvent<T>();
                Cast<T>(thisEvent).AddListener(listener);
                Instance.events1Arg.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// Remove a listener with one argument
        /// </summary>
        public static void StopListening<T>(EventTypes eventName, UnityAction<T> listener)
        {
            if (manager == null) return;
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events1Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast<T>(thisEvent).RemoveListener(listener);
            }
        }

        /// <summary>
        /// Trigger an event with 1 argument
        /// </summary>
        public static void Trigger<T>(EventTypes eventName, T argument)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events1Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast<T>(thisEvent).Invoke(argument);
            }
        }

        /// <summary>
        /// Register a listener for an event using 2 arguments.
        /// </summary>
        public static void StartListening<T0, T1>(EventTypes eventName, UnityAction<T0, T1> listener)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events2Arg.TryGetValue(eventName, out thisEvent))
            {
                if (!(thisEvent is ConcreteUnityEvent<T0, T1>))
                {
                    Debug.LogError("This Key is already used for an event with a different signiture: " + eventName);
                    return;
                }

                Cast<T0, T1>(thisEvent).RemoveListener(listener); // Make sure the same listener is never added more than once
                Cast<T0, T1>(thisEvent).AddListener(listener);
            }
            else
            {
                thisEvent = new ConcreteUnityEvent<T0, T1>();
                Cast<T0, T1>(thisEvent).AddListener(listener);
                Instance.events2Arg.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// Remove a listener with 2 arguments
        /// </summary>
        public static void StopListening<T0, T1>(EventTypes eventName, UnityAction<T0, T1> listener)
        {
            if (manager == null) return;
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events2Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast<T0, T1>(thisEvent).RemoveListener(listener);
            }
        }

        /// <summary>
        /// Trigger an event with 2 arguments
        /// </summary>
        public static void Trigger<T0, T1>(EventTypes eventName, T0 arg0, T1 arg1)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events2Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast<T0, T1>(thisEvent).Invoke(arg0, arg1);
            }
        }

        /// <summary>
        /// Register a listener for an event using 3 arguments.
        /// </summary>
        public static void StartListening<T0, T1, T2>(EventTypes eventName, UnityAction<T0, T1, T2> listener)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events3Arg.TryGetValue(eventName, out thisEvent))
            {
                if (!(thisEvent is ConcreteUnityEvent<T0, T1, T2>))
                {
                    Debug.LogError("This Key is already used for an event with a different signiture: " + eventName);
                    return;
                }

                Cast<T0, T1, T2>(thisEvent).RemoveListener(listener); // Make sure the same listener is never added more than once
                Cast<T0, T1, T2>(thisEvent).AddListener(listener);
            }
            else
            {
                thisEvent = new ConcreteUnityEvent<T0, T1, T2>();
                Cast<T0, T1, T2>(thisEvent).AddListener(listener);
                Instance.events3Arg.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// Remove a listener with 3 arguments
        /// </summary>
        public static void StopListening<T0, T1, T2>(EventTypes eventName, UnityAction<T0, T1, T2> listener)
        {
            if (manager == null) return;
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events3Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast<T0, T1, T2>(thisEvent).RemoveListener(listener);
            }
        }

        /// <summary>
        /// Trigger an event with 3 arguments
        /// </summary>
        public static void Trigger<T0, T1, T2>(EventTypes eventName, T0 arg0, T1 arg1, T2 arg2)
        {
            IConcreteUnityEvent thisEvent = null;
            if (Instance.events3Arg.TryGetValue(eventName, out thisEvent))
            {
                Cast<T0, T1, T2>(thisEvent).Invoke(arg0, arg1, arg2);
            }
        }


        //
        // Utility
        //    
        private static ConcreteUnityEvent Cast(IConcreteUnityEvent value)
        {
            return ((ConcreteUnityEvent)value);
        }

        private static ConcreteUnityEvent<T> Cast<T>(IConcreteUnityEvent value)
        {
            return ((ConcreteUnityEvent<T>)value);
        }

        private static ConcreteUnityEvent<T0, T1> Cast<T0, T1>(IConcreteUnityEvent value)
        {
            return ((ConcreteUnityEvent<T0, T1>)value);
        }

        private static ConcreteUnityEvent<T0, T1, T2> Cast<T0, T1, T2>(IConcreteUnityEvent value)
        {
            return ((ConcreteUnityEvent<T0, T1, T2>)value);
        }
    }
}