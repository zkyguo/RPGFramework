using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : BaseSingletonMono<GameRoot>
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        DontDestroyOnLoad(gameObject);
        InitManager();
    }

    private void InitManager()
    {
        BaseManager[] managers = GetComponents<BaseManager>();
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Init();
        }
    }
}