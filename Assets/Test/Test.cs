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

        /*for (int i = 0; i < 10; i++)
        {
             SaveItem svi = SaveManager.CreateSaveItem();
            SaveManager.SaveObject(new SaveTest() { name = i }, svi);
        }*/

        SaveManager.SaveSetting(new SaveTest() { name = 111});

        Debug.Log(SaveManager.LoadingSetting<SaveTest>().name);

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
