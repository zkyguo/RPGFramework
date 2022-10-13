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
            PoolManager.Instance.GetGameobject<Bullet>(Bullet, null);
        }
        
    }
}
