using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class SaveTest
{
    public int name = 11111;
}

public class Test : MonoBehaviour
{
    private void Start()
    {

        SaveItem svi = SaveManager.CreateSaveItem();
        SaveManager.SaveObject(new SaveTest(), svi);

        Debug.Log(SaveManager.LoadObject<SaveTest>(svi.SaveID).name);

        SaveManager.DeleteSaveItem(svi);

        Debug.Log(SaveManager.LoadObject<SaveTest>(svi.SaveID).name); 
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
