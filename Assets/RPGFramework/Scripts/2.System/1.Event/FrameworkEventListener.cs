using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Descrip all mouse event
/// </summary>
public interface IMouseEvent : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler,IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{ 

}


/// <summary>
/// Event manager : Mouse, Collision, Trigger
/// </summary>
public class FrameworkEventListener : MonoBehaviour, IMouseEvent
{
    #region Event encapsculation

    /// <summary>
    /// One time event data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Pool]
    private class FrameworkEventListenerInfo<T>
    {
        //T : Event class( Pointer, Collision, etc)
        //object : Params for action
        public Action<T, object[]> action;
        public object[] args;
        public void Init(Action<T, object[]>action, object[] args)
        {
            this.action = action;
            this.args = args;
        }

        public void TriggerEvent(T eventData)
        {
            action?.Invoke(eventData, args);
        }
    }

    /// <summary>
    /// Data which contains many FrameworkEventListenerInfo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private class FrameworkEventListenerInfos<T>
    {
        //All event
        private List<FrameworkEventListenerInfo<T>> eventList = new List<FrameworkEventListenerInfo<T>>();

        /// <summary>
        /// Add Event lisener
        /// </summary>
        /// <param name="ation"></param>
        /// <param name="args"></param>
        public void AddListener(Action<T, object[]> action, params object[] args)
        {
            FrameworkEventListenerInfo<T> info = ResourceManager.Instance.Load<FrameworkEventListenerInfo<T>>();
            info.Init(action, args);
            eventList.Add(info);
        }

        /// <summary>
        /// Remove Listener
        /// </summary>
        /// <param name="action"></param>
        /// <param name="checkArgs"></param>
        /// <param name="args"></param>
        public void RemoveListener(Action<T, object[]> action, bool checkArgs, params object[] args)
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                //Find the action
                if (eventList[i].action.Equals(action))
                {
                    //if we ONLY want to remove action with specified Args 
                    if(checkArgs && args.Length > 1)
                    {
                        if (args.ArrayEquals(eventList[i].args))
                        {
                            eventList[i].action.ObjectPushPool();
                            eventList.RemoveAt(i);
                            return;
                        }
                    }
                    else
                    {
                        eventList[i].action.ObjectPushPool();
                        eventList.RemoveAt(i);
                        return;
                    }
                }
            }

        }

        /// <summary>
        /// Remove all listener, push all in pool
        /// </summary>
        public void RemoveAll()
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                eventList[i].ObjectPushPool();
            }
            eventList.Clear();
        }

        /// <summary>
        /// Trigger event
        /// </summary>
        /// <param name="eventData"></param>
        public void TriggerEvent(T eventData)
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                eventList[i].TriggerEvent(eventData);
            }
        }
    }

    #endregion
    #region Mouse Events
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Collision Event

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    #endregion

    #region TriggerEvent

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
