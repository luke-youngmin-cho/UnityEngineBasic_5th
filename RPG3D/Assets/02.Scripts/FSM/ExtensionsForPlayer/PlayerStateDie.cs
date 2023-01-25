using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class PlayerStateDie : CharacterStateBase
    {
        public PlayerStateDie(int id, GameObject owner, Func<bool> canExecute, List<KeyValuePair<Func<bool>, int>> transitions, bool hasExitTime) : base(id, owner, canExecute, transitions, hasExitTime)
        {
        }

        public override void Execute()
        {
            base.Execute();
            movement.mode = MovementBase.Mode.Manual;
            animator.SetBool("doDie", true);
        }

        public override void Stop()
        {
            base.Stop();
            animator.SetBool("doDie", false);
        }
    }    
}