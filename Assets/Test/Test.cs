using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public enum TestType
{
    A, B, C
}

public class TestStateBase : StateBase
{
    protected Text text;
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        text = (owner as Test).text;
    }

    public override void UnInit()
    {
        base.UnInit();
        text = null;
        Debug.Log("Test destroy");
    }
}

[Pool]
public class TestA : TestStateBase
{
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        Debug.Log("Init State A");
    }

    public override void Enter()
    {
        Debug.Log("Enter State A");
        text.text = "A";
    }

    public override void Update()
    {
        Debug.Log("Update State A");
    }

    public override void Exit()
    {
        Debug.Log("Exit State A");
    }

  
}

[Pool]
public class TestB : TestStateBase
{
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        Debug.Log("Init State B");
    }

    public override void Enter()
    {
        Debug.Log("Enter State B");
        text.text = "B";
    }

    public override void Update()
    {
        Debug.Log("Update State B");
    }

    public override void Exit()
    {
        Debug.Log("Exit State B");
    }
}

public class Test : MonoBehaviour, IStateMachineOwner
{
    public Text text { get; private set;}
    private StateMachine stateMachine;

    private void Start()
    {
        text = GetComponent<Text>();
        stateMachine = ResourceManager.Load<StateMachine>();
        stateMachine.Init(this);
        stateMachine.SwitchState<TestA>((int)TestType.A);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            stateMachine.SwitchState<TestA>((int)TestType.A);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            stateMachine.Destroy();
            stateMachine = null;
        }

    }


}
