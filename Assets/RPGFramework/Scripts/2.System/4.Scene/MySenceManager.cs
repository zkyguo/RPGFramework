using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene manager use to take place of unity sceneManager
/// </summary>
public class MySenceManager
{

    /// <summary>
    /// Load scene 
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callback"></param>
    public static void LoadScene(string sceneName, Action callback = null)
    {
        SceneManager.LoadScene(sceneName);
        callback?.Invoke();
    }

    /// <summary>
    /// Load scene async
    /// Dispatch automatically into event center, event names "LoadingSceneProgress"
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callback"></param>
    public static void LoadSceneAsync(string sceneName, Action callback = null)
    {
        MonoManager.Instance.CoroutineStart(DoLoadSceneAync(sceneName, callback));
    }

    private static IEnumerator DoLoadSceneAync(string sceneName, Action callback = null)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while (ao.isDone == false)
        {
            //if operation is not finish, update progress to EventManager
            EventManager.EventTrigger("LoadingSceneProgress", ao.progress);
            yield return ao.progress;
        }
        EventManager.EventTrigger<float>("LoadingSceneProgress", 1f);
        callback?.Invoke(); 
    }
}
