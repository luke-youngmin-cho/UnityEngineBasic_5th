using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateBase
{
    public StateIdle(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
    }

    public override bool CanExecute()
    {
        return true;
    }

    public override void Execute()
    {
        base.Execute();
        Animator.Play("Idle");
    }
}
