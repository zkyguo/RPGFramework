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
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.PlayBackgroundAudio("Menu");
        }
        if(Input.GetKeyUp(KeyCode.B))
        {
            AudioManager.Instance.IsPause = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            AudioManager.Instance.IsPause = false;
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
