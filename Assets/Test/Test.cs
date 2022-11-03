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

        SaveManager.SaveObject(new SaveTest() { name = 5 }, 8);
        SaveManager.SaveObject(new SaveTest() { name = 549885 }, 11);
        SaveManager.SaveObject(new SaveTest() { name = 1231 }, 15);
        List<SaveItem> list = SaveManager.GetSaveItems<int>(
            
            saveItem =>
            {
                SaveTest sv = SaveManager.LoadObject<SaveTest>(saveItem);
                if (sv == null) return 0;
                return sv.name;
            }
            );
        foreach (var item in list)
        {
            Debug.Log(item.SaveID);
        }


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
