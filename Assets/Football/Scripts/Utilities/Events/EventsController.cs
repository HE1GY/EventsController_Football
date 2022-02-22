using System;
using System.Collections.Generic;

namespace Core
{
    public static class EventsController
    {
        private static Dictionary<EventsType, List<Delegate>> events = new Dictionary<EventsType, List<Delegate>>();

        //Add
        public static void AddListener(EventsType eventName, Action callback)
        {
            SetupEvent(eventName);

            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        public static void AddListener<T>(EventsType eventName, Action<T> callback)
        {
            SetupEvent(eventName);
            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        public static void AddListener<T, U>(EventsType eventName, Action<T, U> callback)
        {
            SetupEvent(eventName);
            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        public static void AddListener<T, U, R>(EventsType eventName, Action<T, U, R> callback)
        {
            SetupEvent(eventName);
            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        //Remove
        public static void RemoveListener(EventsType eventName, Action callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Remove(callback);
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T>(EventsType eventName, Action<T> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Add(callback);
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T, U>(EventsType eventName, Action<T, U> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Add(callback);
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T, U, R>(EventsType eventName, Action<T, U, R> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Add(callback);
                ListenerRemoved(eventName);
            }
        }

        private static void ListenerRemoved(EventsType eventName)
        {
            if (CallCondition(eventName))
                events.Remove(eventName);
        }


        //Invoke
        public static void Broadcast(EventsType eventName)
        {
            if (CallCondition(eventName))
                foreach (var item in events[eventName])
                {
                    ((Action)item)();
                }
        }

        public static void Broadcast<T>(EventsType eventName, T param)
        {
            if (CallCondition(eventName))
                foreach (var item in events[eventName])
                {
                    ((Action)item)();
                }
        }

        public static void Broadcast<T, U>(EventsType eventName, T param, U param2)
        {
            if (CallCondition(eventName))
                foreach (var item in events[eventName])
                {
                    ((Action)item)();
                }
        }

        //check null
        private static bool CallCondition(EventsType eventName)
        {
            return events.ContainsKey(eventName) && events[eventName] != null;
        }

        
        internal static void AddListener(EventsType checkLevel)//?
        {
            throw new NotImplementedException();
        }

        private static void SetupEvent(EventsType eventName)
        {
            if (!events.ContainsKey(eventName))
                events.Add(eventName, null);
        }
    }
}