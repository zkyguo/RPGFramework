using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyBullet", 1);
    }

    void DestroyBullet()
    {
        PoolManager.Instance.PushGameobject(gameObject);
    }
}
