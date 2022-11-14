using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Framework
{

    /// <summary>
    /// UI element attribute, should be add to all UI window
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UIElementAttribute : Attribute
    {
        public bool isCache;
        public string resourcePath;
        public int LayerNum;

        public UIElementAttribute(bool isCache, string resourcePath, int layerNum)
        {
            this.isCache = isCache;
            this.resourcePath = resourcePath;
            LayerNum = layerNum;
        }
    }
}
