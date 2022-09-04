using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameObjectPoolData
{
    //Pool Root
    public GameObject RootNode;
    
    //Object list
    public Queue<GameObject> PoolQueue;

    public GameObjectPoolData(GameObject obj, GameObject RootNodeWanted)
    {
        //Create root node and attach it to Scene
        RootNode = new GameObject(obj.name);
        RootNode.transform.SetParent(RootNodeWanted.transform);
        
        this.PoolQueue = new Queue<GameObject>();
        PushObj(obj);
    }

    /// <summary>
    /// Push gameobject to pool
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        // add object to queue
        PoolQueue.Enqueue(obj);
        // attach it to rootNode in Scene
        obj.transform.SetParent(RootNode.transform);
        // set it Invisible
        obj.SetActive(false);
    }

    /// <summary>
    /// take out gameobject of pool
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = PoolQueue.Dequeue();
        // Display obj
        obj.SetActive(true);
        // Dettach obj from parent
        obj.transform.parent = null;
        // Return to default Scence
        SceneManager.MoveGameObjectToScene(obj,  SceneManager.GetActiveScene());
        
        return obj;
    }
}
