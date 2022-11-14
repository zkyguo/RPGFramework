
using UnityEditor;
using UnityEngine;

namespace Framework { 

public class GameRoot : BaseSingletonMono<GameRoot>
{
    [SerializeField]
    private GameSetting _gameSetting;
    public GameSetting gameSetting { get { return _gameSetting; } }

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

#if UNITY_EDITOR

    [InitializeOnLoadMethod]
    public static void InitEditor()
    {
        //Check if editor is playing(in game) or will play soon
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }
        if(Instance == null && GameObject.Find("GameRoot") != null)
        {
            Instance = GameObject.Find("GameRoot").GetComponent<GameRoot>();
            EventManager.Clear();
            Instance.InitManager();
            Instance.gameSetting.InitPoolAttribute();
            
        }
        
    }   

#endif
}

}