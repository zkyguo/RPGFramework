using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Bullet bullet;
    private void Start()
    {
       
        Debug.Log("Start");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && bullet == null) {

            ResourceManager.Instance.LoadGameObjectAsync<Bullet>("Cube", Call);
        }
        if(Input.GetKeyDown(KeyCode.B) && bullet != null)
        {
            PoolManager.Instance.PushGameobject(bullet.gameObject);
            bullet = null;
        }
    }

    private void Call(Bullet obj)
    {
        Debug.Log("Cube Call");
    }
}
