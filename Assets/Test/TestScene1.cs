using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            UIManager.Instance.Show<UILoadingWindow>();
            MySenceManager.LoadSceneAsync("Game", Callback);
        }
    }

    private void Callback()
    {
        UIManager.Instance.Close<UILoadingWindow>();
    }
}
