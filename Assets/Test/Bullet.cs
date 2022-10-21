using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Pool]
public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Cube Awake");
    }

    private void OnEnable()
    {
        Invoke("DestroyBullet", 5);
    }

    void DestroyBullet()
    {
        PoolManager.Instance.PushGameobject(gameObject);
    }


}
