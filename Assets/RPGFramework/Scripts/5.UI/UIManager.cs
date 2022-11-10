using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseManager<UIManager>
{
    /// <summary>
    /// UIElement dic
    /// </summary>
    public Dictionary<Type, UIElement> UIElementsDic { get { return GameRoot.Instance.gameSetting.UIElementCacheDic; } }
}
