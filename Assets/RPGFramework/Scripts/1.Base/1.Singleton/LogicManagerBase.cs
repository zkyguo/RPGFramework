using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// Game logic Manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LogicManagerBase<T> : BaseSingletonMono<T> where T : LogicManagerBase<T>
    {
        /// <summary>
        /// Register event listener 
        /// </summary>
        protected abstract void AddEventListener();
        /// <summary>
        /// Unregister event listener
        /// </summary>
        protected abstract void RemoveEventListener();

        protected virtual void OnEnable()
        {
            AddEventListener();
        }

        protected virtual void OnDisable()
        {
            RemoveEventListener();
        }
    }
}