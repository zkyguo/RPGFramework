using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class of State
/// </summary>
public abstract class StateBase
{
    /// <summary>
    /// StateMachine which state belongs
    /// </summary>
    protected StateMachine stateMachine;

    /// <summary>
    /// Init State
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="stateType"></param>
    /// <param name="stateMachine"></param>
    public virtual void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    /// <summary>
    /// Execute if state will no more be used and return to pool
    /// </summary>
    public virtual void UnInit()
    {
        //push to objectPool
        this.ObjectPushPool();
    }

    /// <summary>
    /// Excute when enter into State
    /// </summary>
    public virtual void Enter() { }

    /// <summary>
    /// Execute during state 
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Execute during state 
    /// </summary>
    public virtual void LateUpdate() { }

    /// <summary>
    /// Execute during state 
    /// </summary>
    public virtual void FixedUpdate() { }

    /// <summary>
    /// Execute before exit state
    /// </summary>
    public virtual void Exit() { }
}
