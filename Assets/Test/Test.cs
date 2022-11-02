using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class SaveTest
{
    public int name;
}

public class Test : MonoBehaviour
{
    private void Start()
    {

        var loading = SaveManager.LoadObject<SaveTest>("UserInfo", 3);

        Debug.Log(loading.name);
    } 

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.PlayEffectAudio("cannon_01", Vector3.zero, 1, true, Call, 1);
        }
    
    }

    private void Call()
    {
        Debug.Log("Audio finished");
    }

    private void Appeler()
    {
        Debug.Log("Test appeler");
    }

}
