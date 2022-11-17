using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateBase
{
    public StateMove(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
    }

    public override bool CanExecute()
    {
        // todo -> check ground
        return true;
    }
}
