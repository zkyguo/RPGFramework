using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class FrameworkEventListenerExtension
{
    #region Tool
    private static FrameworkEventListener Getlistener(Component comp)
    {
        FrameworkEventListener listener = comp.GetComponent<FrameworkEventListener>();
        if (listener == null)
        {
           return comp.gameObject.AddComponent<FrameworkEventListener>();        
        }

        return listener;
    }

    public static void AddEventListener<T>(this Component comp, FrameworkEventType eventType, Action<T, object[]> action, params object[] args)
    {
        FrameworkEventListener listener = Getlistener(comp);
        listener.AddListener(eventType, action, args);
    }

    public static void RemoveEventListener<T>(this Component comp, FrameworkEventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
    {
        FrameworkEventListener listener = Getlistener(comp);
        listener.RemoveListener(eventType, action, checkArgs, args);
    }

    public static void RemoveAllListener(this Component comp, FrameworkEventType eventType)
    {
        FrameworkEventListener listener = Getlistener(comp);
        listener.RemoveAllListener(eventType);
    }

    public static void RemoveAllListener(this Component comp)
    {
        FrameworkEventListener listener = Getlistener(comp);
        listener.RemoveAllListener();
    }

    #endregion

    #region Mouse Event
    public static void OnMouseEnter(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnMouseEnter, action, args);
    }
    public static void OnMouseExit(this Component com, Action<PointerEventData, object[]> action,params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnMouseExit, action, args);
    }
    public static void OnClick(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnClick, action, args);
    }
    public static void OnClickDown(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnClickDown, action, args);
    }
    public static void OnClickUp(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnClickUp, action, args);
    }
    public static void OnDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnDrag, action, args);
    }
    public static void OnBeginDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnBeginDrag, action, args);
    }
    public static void OnEndDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnEndDrag, action, args);
    }
    public static void RemoveClick(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnClick, action, checkArgs, args);
    }
    public static void RemoveClickDown(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnClickDown, action, checkArgs, args);
    }
    public static void RemoveClickUp(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnClickUp, action, checkArgs, args);
    }
    public static void RemoveDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnDrag, action, checkArgs, args);
    }
    public static void RemoveBeginDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnBeginDrag, action, checkArgs, args);
    }
    public static void RemoveEndDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnEndDrag, action, checkArgs, args);
    }


    #endregion

    #region Collision Event

    public static void OnCollisionEnter(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        com.AddEventListener(FrameworkEventType.OnCollisionEnter, action, args);
    }
    public static void OnCollisionStay(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnCollisionStay, action, args);
    }
    public static void OnCollisionExit(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnCollisionExit, action, args);
    }
    public static void OnCollisionEnter2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnCollisionEnter2D, action, args);
    }
    public static void OnCollisionStay2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnCollisionStay2D, action, args);
    }
    public static void OnCollisionExit2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnCollisionExit2D, action, args);
    }
    public static void RemoveCollisionEnter(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnCollisionEnter, action, checkArgs, args);
    }
    public static void RemoveCollisionStay(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnCollisionStay, action, checkArgs, args);
    }
    public static void RemoveCollisionExit(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnCollisionExit, action, checkArgs, args);
    }
    public static void RemoveCollisionEnter2D(this Component com, Action<Collision2D, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnCollisionEnter2D, action, checkArgs, args);
    }
    public static void RemoveCollisionStay2D(this Component com, Action<Collision2D, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnCollisionStay2D, action, checkArgs, args);
    }
    public static void RemoveCollisionExit2D(this Component com, Action<Collision2D, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnCollisionExit2D, action, checkArgs, args);
    }
    #endregion

    #region Trigger Event
    public static void OnTriggerEnter(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnTriggerEnter, action, checkArgs, args);
    }
    public static void OnTriggerStay(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnTriggerStay, action, checkArgs, args);
    }
    public static void OnTriggerExit(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnTriggerExit, action, checkArgs, args);
    }
    public static void OnTriggerEnter2D(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnTriggerEnter2D, action, checkArgs, args);
    }
    public static void OnTriggerStay2D(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnTriggerStay2D, action, checkArgs, args);
    }
    public static void OnTriggerExit2D(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        AddEventListener(com, FrameworkEventType.OnTriggerExit2D, action, checkArgs, args);
    }
    public static void RemoveTriggerEnter(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnTriggerEnter, action, checkArgs, args);
    }
    public static void RemoveTriggerStay(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnTriggerStay, action, checkArgs, args);
    }
    public static void RemoveTriggerExit(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnTriggerExit, action, checkArgs, args);
    }
    public static void RemoveTriggerEnter2D(this Component com, Action<Collider2D, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnTriggerEnter2D, action, checkArgs, args);
    }
    public static void RemoveTriggerStay2D(this Component com, Action<Collider2D, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnTriggerStay2D, action, checkArgs, args);
    }
    public static void RemoveTriggerExit2D(this Component com, Action<Collider2D, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameworkEventType.OnTriggerExit2D, action, checkArgs, args);
    }
    #endregion
}