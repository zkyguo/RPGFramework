using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.Accessibility;

namespace Framework
{
    public class PoolManager : BaseManager<PoolManager>
    {
        //Root
        [SerializeField]
        private GameObject PoolRootObj;

        /// <summary>
        /// container of gameobject
        /// </summary>
        public Dictionary<string, GameObjectPoolData> GameObjectPoolDic = new Dictionary<string, GameObjectPoolData>();
        /// <summary>
        /// container of non-gameObject pool
        /// </summary>
        public Dictionary<string, ObjectPoolData> ObjectPoolDic = new Dictionary<string, ObjectPoolData>();

        public override void Init()
        {
            base.Init();

        }

        #region GameObject operations
        /// <summary>
        /// Get component, per exemple :
        /// Bullet has bullet script/component
        /// Enemy has Enemy script/component
        /// </summary>
        /// <param name="prefab"></param>
        /// <typeparam name="T">The component which you need</typeparam>
        /// <returns></returns>
        public T GetGameobject<T>(GameObject prefab, Transform parent = null) where T : UnityEngine.Object
        {
            GameObject obj = GetGameobject(prefab, parent);
            if (obj != null)
            {
                return obj.GetComponent<T>();
            }

            return null;
        }

        /// <summary>
        /// Get component
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public GameObject GetGameobject(GameObject prefab, Transform parent = null)
        {
            GameObject obj = null;
            string name = prefab.name;

            //Check if gameObject exists in pool
            if (CheckGameobjectCache(prefab))
            {
                obj = GameObjectPoolDic[name].GetObj(parent);
            }
            //if not, instantiate it
            else
            {
                obj = GameObject.Instantiate(prefab, parent);
                obj.name = name;
            }
            return obj;
        }

        /// <summary>
        /// Push gameobject to object Pool (when object finishs its mission)
        /// </summary>
        /// <param name="obj"></param>
        public void PushGameobject(GameObject obj)
        {
            string name = obj.name;
            //check if obj exists in pool
            if (GameObjectPoolDic.ContainsKey(name))
            {
                GameObjectPoolDic[name].PushObj(obj);
            }
            //if not, create it
            else
            {
                GameObjectPoolDic.Add(name, new GameObjectPoolData(obj, PoolRootObj));
            }

        }

        /// <summary>
        /// Check if data exists in pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        private bool CheckGameobjectCache(GameObject prefab)
        {
            string name = prefab.name;
            return GameObjectPoolDic.ContainsKey(name) && GameObjectPoolDic[name].PoolQueue.Count > 0;
        }


        /// <summary>
        /// Check if gameObject exists in cache, if yes load it
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject CheckCacheAndLoadGameObject(string path, Transform parent = null)
        {
            //using path to get prefab name
            string[] pathSplit = path.Split('/');
            string prefabName = pathSplit[pathSplit.Length - 1];
            //Get prefab in pool by prefabName found
            if (GameObjectPoolDic.ContainsKey(prefabName) && GameObjectPoolDic[prefabName].PoolQueue.Count > 0)
            {
                return GameObjectPoolDic[prefabName].GetObj();
            }

            return null;
        }
        #endregion

        #region Non-gameObject Operations

        /// <summary>
        /// Get non-game object from non-game object pool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObject<T>() where T : class, new()
        {
            T obj;
            //check if obj exists in pool
            if (CheckObjectCache<T>())
            {
                string name = typeof(T).FullName;
                obj = ObjectPoolDic[name].GetObj() as T;
                return obj;
            }
            //if not, create new one 
            return new T();
        }

        /// <summary>
        /// Push object to pool(when finish its mission)
        /// </summary>
        /// <param name="obj"></param>
        public void PushObject(object obj)
        {
            string name = obj.GetType().FullName;
            if (ObjectPoolDic.ContainsKey(name))
            {
                ObjectPoolDic[name].PushObj(obj);
            }
            else
            {
                ObjectPoolDic.Add(name, new ObjectPoolData(obj));
            }
        }


        /// <summary>
        /// Check if data exists in pool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private bool CheckObjectCache<T>()
        {
            string name = typeof(T).FullName;
            return ObjectPoolDic.ContainsKey(name) && ObjectPoolDic[name].PoolQueue.Count > 0;
        }
        #endregion

        #region Clear 
        /// <summary>
        /// Clear pool
        /// </summary>
        /// <param name="clearGameObject">Should clear GameObject pool?</param>
        /// <param name="clearObject">Should clear non-GameObject pool?</param>
        public void Clear(bool clearGameObject = true, bool clearObject = true)
        {
            if (clearGameObject)
            {
                for (int i = 0; i < PoolRootObj.transform.childCount; i++)
                {
                    Destroy(PoolRootObj.transform.GetChild(0).gameObject);
                }
                GameObjectPoolDic.Clear();
            }
            if (clearObject) ObjectPoolDic.Clear();
        }

        public void ClearGameOjectPool()
        {
            Clear(true, false);
        }

        public void ClearGameObject(string prefabName)
        {
            GameObject obj = PoolRootObj.transform.Find(prefabName).gameObject;
            if (obj != null)
            {
                Destroy(obj.gameObject);
                GameObjectPoolDic.Remove(prefabName);
            }
        }

        public void ClearGameObject(GameObject prefab)
        {
            ClearGameObject(prefab.name);
        }

        public void ClearObjectPool()
        {
            Clear(false, true);
        }

        public void ClearObject<T>()
        {
            ObjectPoolDic.Remove(typeof(T).FullName);
        }

        public void ClearObject(Type type)
        {
            ObjectPoolDic.Remove(type.FullName);
        }
        #endregion
    }
}