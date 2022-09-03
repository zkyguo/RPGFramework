using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class of singleton
/// </summary>
public class BaseSingleton<T> where T : BaseSingleton<T>, new() //new() : T must can be instantiated.
                                                                //BaseSingleton<T> : T must be a child of BaseSingleton
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }

            return instance;
        }
        
    }
}
