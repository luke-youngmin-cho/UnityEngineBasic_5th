using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class PlayerStateMove : CharacterStateBase
    {
        public PlayerStateMove(int id, GameObject owner, Func<bool> canExecute, List<KeyValuePair<Func<bool>, int>> transitions, bool hasExitTime) : base(id, owner, canExecute, transitions, hasExitTime)
        {
        }

        public override void Execute()
        {
            base.Execute();
            movement.mode = MovementBase.Mode.RootMotion;
            animator.SetBool("doMove", true);
        }

        public override void Stop()
        {
            base.Stop();
            animator.SetBool("doMove", false);
        }
    }

}
