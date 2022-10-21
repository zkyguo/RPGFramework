using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Bullet bullet = ResourceManager.Instance.Load<Bullet>("Cube");
        bullet.OnClick(Click,"String",111,110);
        bullet.OnClick(Click, "String123", 123, 321);
        bullet.OnClick(Click, "String321", 123, 321);
        bullet.OnMouseEnter(Enter);
        bullet.RemoveAllListener();

    }

    private void Call(Bullet obj)
    {
        Debug.Log("Cube Call");
    }

    void Click(PointerEventData data ,params object[] args)
    {
        Debug.Log("Click on :" + data.position);
        Debug.Log(args[0]);
        Debug.Log(args[1]);
        Debug.Log(args[2]);
    }

    void Enter(PointerEventData data ,params object[] args)
    {
        Debug.Log("Mouse Enter");
    }
}
