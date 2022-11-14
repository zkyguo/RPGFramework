
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    public class MonoManager : BaseManager<MonoManager>
    {
        private Action updateEvent;
        private Action LateUpdateEvent;
        private Action FixedUpdateEvent;

        /// <summary>
        /// Add update Listener 
        /// </summary>
        /// <param name="action"></param>
        public void AddUpdateListener(Action action)
        {
            updateEvent += action;
        }

        /// <summary>
        /// Add LateUpdate Listener 
        /// </summary>
        /// <param name="action"></param>
        public void AddLateUpdateListener(Action action)
        {
            LateUpdateEvent += action;
        }

        /// <summary>
        /// Add FixedUpdate Listener 
        /// </summary>
        /// <param name="action"></param>
        public void AddFixedUpdateListener(Action action)
        {
            FixedUpdateEvent += action;
        }

        /// <summary>
        /// Remove updateListener of MonoManager
        /// </summary>
        /// <param name="action"></param>
        public void RemoveUpdateListener(Action action)
        {
            updateEvent -= action;
        }

        /// <summary>
        /// Remove LateUpdate Listener of MonoManager
        /// </summary>
        /// <param name="action"></param>
        public void RemoveLateUpdateListener(Action action)
        {
            LateUpdateEvent -= action;
        }

        /// <summary>
        /// Remove FixedUpdate Listener of MonoManager
        /// </summary>
        /// <param name="action"></param>
        public void RemoveFixedUpdateListener(Action action)
        {
            FixedUpdateEvent -= action;
        }


        /// <summary>
        /// Execute updateEvent at each frame
        /// </summary>
        private void Update()
        {
            updateEvent?.Invoke();

        }

        /// <summary>
        /// Execute FixedupdateEvent at each frame
        /// </summary>
        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();

        }

        /// <summary>
        /// Execute LateupdateEvent at each frame
        /// </summary>
        private void LateUpdate()
        {
            LateUpdateEvent?.Invoke();

        }
    }

}
