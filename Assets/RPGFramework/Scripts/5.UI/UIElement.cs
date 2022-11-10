using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Element of UI
/// </summary>
public class UIElement 
{
    [LabelText("Need cache?")]
    public bool isCache;
    [LabelText("Need prefab?")]
    public GameObject prefab;
    [LabelText("UI layer level")]
    public int layerNum;

    /// <summary>
    /// windowbase which this element matches
    /// </summary>
    [HideInInspector]
    public UIWindowsBase ObjInstance;
}
