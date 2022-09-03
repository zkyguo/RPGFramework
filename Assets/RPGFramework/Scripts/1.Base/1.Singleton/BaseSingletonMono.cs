using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSingletonMono<T> : MonoBehaviour where T :  BaseSingletonMono<T>
{
    public static T instance;

    protected virtual void Awake() // protected : Child can acces to this method. Virtual : child can override this method
    {
        instance = this as T;
    }
}
