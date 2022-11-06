
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : BaseManager<MonoManager>
{
    private Action updateEvent;

    /// <summary>
    /// Add updateListener to MonoManager
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateListener(Action action)
    {
        updateEvent += action;
    }

    /// <summary>
    /// Remove updateListener of MonoManager
    /// </summary>
    /// <param name="action"></param>
    public void RemoveUpdateListener(Action action)
    {
        updateEvent -= action;
    }

    /// <summary>
    /// Execute updateEvent at each frame
    /// </summary>
    private void Update()
    {
        updateEvent?.Invoke();
       
    }

}
