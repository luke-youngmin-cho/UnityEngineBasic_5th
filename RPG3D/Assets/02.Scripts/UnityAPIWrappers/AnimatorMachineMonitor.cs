using System.Collections;
using System;
using UnityEngine;

/// <summary>
/// 애니메이터의 sub-state machine 용 감시자
/// </summary>
public class AnimatorMachineMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);
        OnEnter?.Invoke(stateMachinePathHash);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);
        OnExit?.Invoke(stateMachinePathHash);
    }
}
