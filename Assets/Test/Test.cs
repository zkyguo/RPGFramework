using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TestModel
{
    public TestModel()
    {
        this.AddUpdateListener(OnUpdate);
   

    }

    private void OnUpdate()
    {
        Debug.Log("OnUpdate");
        if (Input.GetKeyDown(KeyCode.A))
        {


        }
    }
}

public class Test : MonoBehaviour
{
  
    private void Start()
    {
        EventManager.AddEventListener<float>("LoadingSceneProgress", LoadScene);
        MySenceManager.LoadScene("SceneTest", CallBack);
        

    }

    private void LoadScene(float obj)
    {
        Debug.Log("progress : " + obj);
    }

    void CallBack() 
    {
        Debug.Log("Scene Loaded");
    }


    private void Update()
    {
        
        
    
    }


}
