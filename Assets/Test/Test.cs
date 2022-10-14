using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Bullet;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Add");
            var obj = PoolManager.Instance.GetGameobject(Bullet);
            PoolManager.Instance.PushGameobject(obj);
           
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Clear");
            PoolManager.Instance.ClearGameOjectPool();
        }
    }
}
