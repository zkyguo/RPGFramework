using System;
using System.Collections.Generic;


public class ObjectPoolData
{
    //non-gameObject pool qeueu
    public Queue<object> PoolQueue = new Queue<object>();

    public ObjectPoolData(object obj)
    {
        PushObj(obj);
    }
    
    /// <summary>
    /// Push non-gameObject object to poool
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(object obj)
    {
        PoolQueue.Enqueue(obj);
    }

    /// <summary>
    /// Get object from pool
    /// </summary>
    /// <returns></returns>
    public object GetObj()
    {
        return PoolQueue.Dequeue();
    }
}
