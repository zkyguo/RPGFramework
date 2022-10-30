using System;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Event system management
/// </summary>
public static class EventManager 
{
    #region Internal

    /// <summary>
    /// Event Info interface
    /// </summary>
    private interface IEventInfo
    {
        void Destroy();
    };

    /// <summary>
    /// Event data for no type
    /// </summary>
    private class EventInfo : IEventInfo
    {
        public Action action;
        public void Init(Action action)
        {
            this.action = action;
        }

        public void Destroy()
        { 
            this.action = null;
            this.ObjectPushPool();
        } 
    }

    /// <summary>
    /// Event data for one type
    /// </summary>
    private class EventInfo<T> : IEventInfo
    {
        public Action<T> action;

        public void Destroy()
        {
            this.action = null;
            this.ObjectPushPool();
        }

        public void Init(Action<T> action)
        {
            this.action = action;
        }
    }

    /// <summary>
    /// Event data for 2 types
    /// </summary>
    private class EventInfo<T, K> : IEventInfo
    {
        public Action<T, K> action;

        public void Destroy()
        {
            this.action = null;
            this.ObjectPushPool();
        }

        public void Init(Action<T, K> action)
        {
            this.action = action;
        }
    }

    /// <summary>
    /// Event data for 3 types
    /// </summary>
    private class EventInfo<T, K, L> : IEventInfo
    {
        public Action<T, K, L> action;

        public void Destroy()
        {
            this.action = null;
            this.ObjectPushPool();
        }

        public void Init(Action<T, K, L> action)
        {
            this.action = action;
        }
    }

    private static Dictionary<string, IEventInfo> eventInfoDic = new Dictionary<string, IEventInfo>();
    #endregion

    #region Event listener

    /// <summary>
    /// Add no-type Event listener
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public static void AddEventListener(string eventName, Action action)
    {
        if(eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo).action += action;
        }
        else
        {
            EventInfo eventInfo = PoolManager.Instance.GetObject<EventInfo>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    /// <summary>
    /// Add one type Event listener
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public static void AddEventListener<T>(string eventName, Action<T> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T>).action += action;
        }
        else
        {
            EventInfo<T> eventInfo = PoolManager.Instance.GetObject<EventInfo<T>>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    /// <summary>
    /// Add two type Event listener
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public static void AddEventListener<T, K>(string eventName, Action<T, K> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K>).action += action;
        }
        else
        {
            EventInfo<T, K> eventInfo = PoolManager.Instance.GetObject<EventInfo<T, K>>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    /// <summary>
    /// Add two type Event listener
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public static void AddEventListener<T, K, L>(string eventName, Action<T, K, L> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K, L>).action += action;
        }
        else
        {
            EventInfo<T, K, L> eventInfo = PoolManager.Instance.GetObject<EventInfo<T, K, L>>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }
    #endregion

    #region Event Trigger

    /// <summary>
    /// Trigger event with no generic 
    /// </summary>
    /// <param name="eventName"></param>
    public static void EventTrigger(string eventName)
    {
        if(eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo).action?.Invoke();
        }
    }

    /// <summary>
    /// Trigger event with T  
    /// </summary>
    /// <param name="eventName"></param>
    public static void EventTrigger<T>(string eventName, T arg)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T>).action?.Invoke(arg);
        }
    }

    /// <summary>
    /// Trigger event with T ? K 
    /// </summary>
    /// <param name="eventName"></param>
    public static void EventTrigger<T,K>(string eventName, T arg1, K arg2)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K>).action?.Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// Trigger event with T, K, L
    /// </summary>
    /// <param name="eventName"></param>
    public static void EventTrigger<T, K, L>(string eventName, T arg1, K arg2, L arg3)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K, L>).action?.Invoke(arg1, arg2, arg3);
        }
    }
    #endregion

    #region Cancel event listening action

    /// <summary>
    /// Cancel no generic event listener
    /// </summary>
    /// <param name="eventName"></param>
    public static void CancelEventListener(string eventName, Action action)
    {
        if(eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo).action -= action;
        }
    }

    /// <summary>
    /// Cancel T event listener
    /// </summary>
    /// <param name="eventName"></param>
    public static void CancelEventListener<T>(string eventName, Action<T> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T>).action -= action;
        }
    }

    /// <summary>
    /// Cancel T, K event listener
    /// </summary>
    /// <param name="eventName"></param>
    public static void CancelEventListener<T, K>(string eventName, Action<T, K> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K>).action -= action;
        }
    }


    /// <summary>
    /// Cancel T, K, L event listener
    /// </summary>
    /// <param name="eventName"></param>
    public static void CancelEventListener<T, K, L>(string eventName, Action<T, K, L> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K, L>).action -= action;
        }
    }


    #endregion

    #region Remove listening Event

    /// <summary>
    /// Delete an Event from Dic
    /// </summary>
    /// <param name="eventName"></param>
    public static void RemoveEventListener(string eventName)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            //Put it in Object pool, maybe reuse
            eventInfoDic[eventName].Destroy();
            eventInfoDic.Remove(eventName);
        }
         
    }

    /// <summary>
    /// Delete all event in Dic
    /// </summary>
    public static void Clear()
    {
        foreach (var eventName in eventInfoDic.Keys)
        {
            eventInfoDic[eventName].Destroy();
        }
        eventInfoDic.Clear();
    }

    #endregion


}
