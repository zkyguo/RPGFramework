using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{

    /// <summary>
    /// Descrip all mouse event
    /// </summary>
    public interface IMouseEvent : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

    }

    /// <summary>
    /// Event Type
    /// </summary>
    public enum FrameworkEventType
    {
        OnMouseEnter,
        OnMouseExit,
        OnClick,
        OnClickDown,
        OnClickUp,
        OnDrag,
        OnBeginDrag,
        OnEndDrag,
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnCollisionEnter2D,
        OnCollisionStay2D,
        OnCollisionExit2D,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit,
        OnTriggerEnter2D,
        OnTriggerStay2D,
        OnTriggerExit2D
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
        private class FrameworkEventListenerInfo<T>
        {
            //T : Event class( Pointer, Collision, etc)
            //object : Params for action
            public Action<T, object[]> action;
            public object[] args;
            public void Init(Action<T, object[]> action, object[] args)
            {
                this.action = action;
                this.args = args;
            }

            public void Destory()
            {
                this.action = null;
                this.args = null;
                this.ObjectPushPool();
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
        private class FrameworkEventListenerInfos<T> : IFrameworkEventListenerInfos
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
                FrameworkEventListenerInfo<T> info = PoolManager.Instance.GetObject<FrameworkEventListenerInfo<T>>();
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
                        if (checkArgs && args.Length > 1)
                        {
                            if (args.ArrayEquals(eventList[i].args))
                            {
                                eventList[i].Destory();
                                eventList.RemoveAt(i);
                                return;
                            }
                        }
                        else
                        {
                            eventList[i].Destory();
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
                this.ObjectPushPool();
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

        interface IFrameworkEventListenerInfos
        {

            void RemoveAll();
        }

        /// <summary>
        /// Enum comparator
        /// </summary>
        private class FrameworkEventTypeEnumComparer : BaseSingleton<FrameworkEventTypeEnumComparer>, IEqualityComparer<FrameworkEventType>
        {
            public bool Equals(FrameworkEventType x, FrameworkEventType y)
            {
                return x == y;
            }

            public int GetHashCode(FrameworkEventType obj)
            {
                return (int)obj;
            }
        }
        #endregion



        private Dictionary<FrameworkEventType, IFrameworkEventListenerInfos> eventInfoDic = new Dictionary<FrameworkEventType, IFrameworkEventListenerInfos>(FrameworkEventTypeEnumComparer.Instance);

        #region Externel acces

        /// <summary>
        /// Add event listener 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <param name="checkArgs"></param>
        /// <param name="args"></param>
        public void AddListener<T>(FrameworkEventType eventType, Action<T, object[]> action, params object[] args)
        {
            //if event exists in pool, get it
            if (eventInfoDic.ContainsKey(eventType))
            {
                var eventInfos = eventInfoDic[eventType] as FrameworkEventListenerInfos<T>;
                eventInfos.AddListener(action, args);
            }
            //if not add it in pool, infos and Dic
            else
            {
                FrameworkEventListenerInfos<T> infos = PoolManager.Instance.GetObject<FrameworkEventListenerInfos<T>>();
                infos.AddListener(action, args);
                eventInfoDic.Add(eventType, infos);
            }

        }

        /// <summary>
        /// Remove Listener
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <param name="checkArgs"></param>
        /// <param name="args"></param>
        public void RemoveListener<T>(FrameworkEventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as FrameworkEventListenerInfos<T>)?.RemoveListener(action, checkArgs, args);
            }
        }

        /// <summary>
        /// Remove All eventType 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        public void RemoveAllListener(FrameworkEventType eventType)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                eventInfoDic[eventType]?.RemoveAll();
                eventInfoDic.Remove(eventType);
            }
        }

        /// <summary>
        /// Clear all event
        /// </summary>
        public void RemoveAllListener()
        {
            foreach (var infos in eventInfoDic.Values)
            {
                (infos as IFrameworkEventListenerInfos).RemoveAll();
            }
            eventInfoDic.Clear();

        }
        #endregion

        /// <summary>
        /// Trigger event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="eventData"></param>
        private void TriggerAction<T>(FrameworkEventType eventType, T eventData)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as FrameworkEventListenerInfos<T>).TriggerEvent(eventData);
            }
        }

        #region Mouse event
        public void OnPointerEnter(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnMouseEnter, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnMouseExit, eventData);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnBeginDrag, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnDrag, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnEndDrag, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnClick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnClickDown, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TriggerAction(FrameworkEventType.OnClickUp, eventData);
        }
        #endregion

        #region Collision Event
        private void OnCollisionEnter(Collision collision)
        {
            TriggerAction(FrameworkEventType.OnCollisionEnter, collision);
        }
        private void OnCollisionStay(Collision collision)
        {
            TriggerAction(FrameworkEventType.OnCollisionStay, collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            TriggerAction(FrameworkEventType.OnCollisionExit, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TriggerAction(FrameworkEventType.OnCollisionEnter2D, collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TriggerAction(FrameworkEventType.OnCollisionStay2D, collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            TriggerAction(FrameworkEventType.OnCollisionExit2D, collision);
        }
        #endregion

        #region Trigger Event
        private void OnTriggerEnter(Collider other)
        {
            TriggerAction(FrameworkEventType.OnTriggerEnter, other);
        }
        private void OnTriggerStay(Collider other)
        {
            TriggerAction(FrameworkEventType.OnTriggerStay, other);
        }
        private void OnTriggerExit(Collider other)
        {
            TriggerAction(FrameworkEventType.OnTriggerExit, other);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerAction(FrameworkEventType.OnTriggerEnter2D, collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            TriggerAction(FrameworkEventType.OnTriggerStay2D, collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerAction(FrameworkEventType.OnTriggerExit2D, collision);
        }


        #endregion

    }
}