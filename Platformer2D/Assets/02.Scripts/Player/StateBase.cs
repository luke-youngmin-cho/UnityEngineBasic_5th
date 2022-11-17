using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public enum Commands
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    protected Commands Current;
    protected StateMachine Machine;
    protected StateMachine.StateTypes Type;
    protected Animator Animator;

    public StateBase(StateMachine.StateTypes type, StateMachine machine)
    {
        Type = type;
        Machine = machine;
        Animator = machine.GetComponent<Animator>();
    }

    public abstract bool CanExecute();

    public virtual void Execute()
    {
        Current = Commands.Prepare;
    }

    public virtual void Stop()
    {
        Current = Commands.Idle;
    }

    public void MoveNext()
    {
        if (Current < Commands.Finish)
            Current++;
    }

    public virtual StateMachine.StateTypes Update()
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
                next = default(StateMachine.StateTypes);
                break;
            default:
                break;
        }

        return next;
    }
}
