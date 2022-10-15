using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Basic Game setting : 
/// Default screen resolution, music, gamplay graphic, UI
/// </summary>
[CreateAssetMenu(fileName = "GameSetting", menuName = "RPGFramework/Config/GameSetting ")]
public class GameSetting : ConfigBase
{
#if UNITY_EDITOR


    [Button(Name = "Initialize GameSetting", ButtonHeight = 50)]
    [GUIColor(0,1,0)]
    private void Init()
    {
        
    }

    /// <summary>
    /// Execute while Editor is loading
    /// </summary>
    [InitializeOnLoadMethod]
    private static void EditorOnLoad()
    {
        GameObject.Find("GameRoot").GetComponent<GameRoot>().GameSetting.Init();
      
    }

#endif
}
