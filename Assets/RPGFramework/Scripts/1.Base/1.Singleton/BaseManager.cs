using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager  : MonoBehaviour
{
    public virtual void Init() { }
}


public abstract class BaseManager<T> : BaseManager where T : BaseManager<T>
{
    public static T Instance;

    public override void Init()
    {
        Instance = this as T;
    }
}
