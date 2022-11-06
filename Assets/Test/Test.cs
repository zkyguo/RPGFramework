using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

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
        ResourceManager.LoadGameObjectAsync<Bullet>("Cube");
        

    }

    void CallBack(Bullet bullet) 
    {
        Debug.Log(bullet.transform.name);
    }


    private void Update()
    {
        
        
    
    }


}
