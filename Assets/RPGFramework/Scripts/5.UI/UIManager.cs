using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : BaseManager<UIManager>
{
    #region Internal

    [Serializable]
    private class UILayer
    {
        public Transform Root;
        public Image MaskImage;
        private int LayerCount = 0;
        public void OnShow()
        {
            LayerCount += 1;
            Update();
        }

        public void OnClose() 
        {
            LayerCount -= 1;
            Update();
        }

        void Update()
        {
            MaskImage.raycastTarget = (LayerCount != 0);
            int posIndex = Root.childCount - 2;
            MaskImage.transform.SetSiblingIndex(posIndex < 0? 0 : posIndex);
        }
    }

    [SerializeField]
    private UILayer[] UILayers;

    #endregion

    /// <summary>
    /// UIElement dic
    /// </summary>
    public Dictionary<Type, UIElement> UIElementsDic { get { return GameRoot.Instance.gameSetting.UIElementCacheDic; } }

    /// <summary>
    /// Show window
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="layer"></param>
    /// <returns></returns>
    public T Show<T>(int layer = -1) where T : UIWindowsBase
    {
        return Show(typeof(T), layer) as T;
    }

    public UIWindowsBase Show(Type type, int layer = -1)
    {

        if (UIElementsDic.ContainsKey(type))
        {
            UIElement info = UIElementsDic[type];
            int layerNum = (layer == -1 ? info.layerNum : layer);
            Debug.Log(layerNum);
            //If UI object exists
            if (info.ObjInstance != null)
            {
                //Active UI object
                info.ObjInstance.gameObject.SetActive(true);
                //Put it under layer which it belongs
                info.ObjInstance.transform.SetParent(UILayers[layerNum].Root);
                //put it before mask
                info.ObjInstance.transform.SetAsLastSibling();
                //Show UI object
                info.ObjInstance.Show();
            }
            //if not
            else
            {
                //get from ResourcesManager
                UIWindowsBase window = ResourceManager.InstantiatePrefab(info.prefab, UILayers[layerNum].Root).GetComponent<UIWindowsBase>();
                info.ObjInstance = window;
                //Init UI object
                window.Init();
                //Show UI object
                info.ObjInstance.Show();
            }

            info.layerNum = layerNum;
            UILayers[layerNum].OnShow();
            return info.ObjInstance;
        }

        return null;
    }

    /// <summary>
    /// Close window
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Close<T>()
    {
        Close(typeof(T));
    }

    /// <summary>
    /// Close window
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Close(Type type)
    {
        if(UIElementsDic.ContainsKey(type))
        {
            UIElement info = UIElementsDic[type];
            if (info.ObjInstance == null) return;

            //if UI object will be used again
            if(info.isCache)
            {
                //hide it 
                info.ObjInstance.gameObject.SetActive(false);
            }
            //if not
            else
            {
                //release it
                Destroy(info.ObjInstance);
                info.ObjInstance = null;
            }
            UILayers[info.layerNum].OnClose();
           

        }
    }

    /// <summary>
    /// Close all window(not destroy)
    /// </summary>
    public void CloseAll()
    {
        var enumerator = UIElementsDic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.ObjInstance.Close();
        }
        
    }
}
