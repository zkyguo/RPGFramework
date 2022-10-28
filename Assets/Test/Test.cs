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
        EventManager.AddEventListener("Test", Call);
        EventManager.AddEventListener("Test", Appeler);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            EventManager.EventTrigger("Test");
        }
        if(Input.GetKeyUp(KeyCode.B))
        {
            
        }
    }

    private void Call()
    {
        Debug.Log("Test");
    }

    private void Appeler()
    {
        Debug.Log("Test appeler");
    }

}
