
using System;
using System.Collections.Generic;

/// <summary>
/// Define if an gameObject can have stateMachine
/// </summary>
public interface IStateMachineOwner { }

/// <summary>
/// StateMachine which manages states within
/// </summary>
[Pool]
public class StateMachine
{
    /// <summary>
    /// Current State of stateMachine
    /// </summary>
    public int CurrentStateType { get; private set; } = -1;

    /// <summary>
    /// Current state playing
    /// </summary>
    private StateBase currentStateObj;

    /// <summary>
    /// Owner of state machine
    /// </summary>
    private IStateMachineOwner Owner;

    /// <summary>
    /// Key : State Enum, Value : State class
    /// </summary>
    private Dictionary<int, StateBase> statesDic = new Dictionary<int, StateBase>();

    /// <summary>
    /// Init stateMachine
    /// </summary>
    /// <param name="owner"></param>
    public void Init(IStateMachineOwner owner)
    {
        this.Owner = owner;
    }

    /// <summary>
    /// Change state
    /// </summary>
    /// <typeparam name="T">new State class</typeparam>
    /// <param name="newState">new state enum</param>
    /// <param name="reCurrState">if new state = state to change, switch or not</param>
    public bool SwitchState<T>(int newStateType, bool reCurrState =false) where T : StateBase, new()
    {
        if (newStateType == CurrentStateType && !reCurrState)
        {
            return false;
        }

        //Exit
        if(currentStateObj != null) 
        {
            currentStateObj.Exit();
            currentStateObj.RemoveUpdateListener(currentStateObj.Update);
            currentStateObj.RemoveLateUpdateListener(currentStateObj.LateUpdate);   
            currentStateObj.RemoveFixedUpdateListener(currentStateObj.FixedUpdate);   
        }
        //Start the new state
        currentStateObj = GetState<T>(newStateType);
        CurrentStateType = newStateType;
        currentStateObj.Enter();
        currentStateObj.AddUpdateListener(currentStateObj.Update);
        currentStateObj.AddLateUpdateListener( currentStateObj.LateUpdate);
        currentStateObj.AddFixedUpdateListener( currentStateObj.FixedUpdate);

        return true;
    }

    /// <summary>
    /// Get state from pool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stateType"></param>
    /// <returns></returns>
    private StateBase GetState<T>(int stateType) where T : StateBase, new()
    {
        if (statesDic.ContainsKey(stateType))
        {
            return statesDic[stateType];
        }

        StateBase state = ResourceManager.Load<T>();
        state.Init(Owner, stateType, this);
        statesDic.Add(stateType, state);

        return state;   

    }

    /// <summary>
    /// Clear and stop stateMachine
    /// </summary>
    public void Clear()
    {
        currentStateObj.Exit();
        currentStateObj.RemoveUpdateListener(currentStateObj.Update);
        currentStateObj.RemoveLateUpdateListener(currentStateObj.LateUpdate);
        currentStateObj.RemoveFixedUpdateListener(currentStateObj.FixedUpdate);
        CurrentStateType = -1;
        currentStateObj = null;
        var enumerator = statesDic.GetEnumerator();
        while(enumerator.MoveNext())
        {
            enumerator.Current.Value.UnInit();
        }
        statesDic.Clear();
    }

    /// <summary>
    /// Destroy StateMachine
    /// </summary>
    public void Destroy()
    {
        Clear();
        Owner = null;
        this.ObjectPushPool();
    }

}
