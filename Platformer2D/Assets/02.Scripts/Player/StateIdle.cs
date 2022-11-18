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
        Movement.DirectionChangable = true;
        Movement.Movable = true;
    }

    public override StateMachine.StateTypes Update()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            case Commands.Prepare:
                MoveNext();
                break;
            case Commands.Casting:
                MoveNext();
                break;
            case Commands.OnAction:
                MoveNext();
                break;
            case Commands.Finish:
                break;
            default:
                break;
        }

        return next;
    }
}

