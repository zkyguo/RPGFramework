using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    

    private void Start()
    {
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.Instance.Show<TestWindow>();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.Instance.Show<TestWindow2>();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.Instance.Close<TestWindow>();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.Instance.Close<TestWindow2>();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.Show<TestWindow>(4);
        }
    }


}
