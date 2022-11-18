using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateBase
{
    private GroundDetector _groudDetector;
    public StateMove(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _groudDetector = machine.GetComponent<GroundDetector>();
    }

    public override bool CanExecute()
    {
        return _groudDetector.IsDetected;
    }

    public override void Execute()
    {
        base.Execute();
        Animator.Play("Move");
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
