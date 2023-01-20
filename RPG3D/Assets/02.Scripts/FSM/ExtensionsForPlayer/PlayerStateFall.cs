using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{

    public class PlayerStateFall : CharacterStateBase
    {
        public PlayerStateFall(int id, GameObject owner, Func<bool> canExecute, List<KeyValuePair<Func<bool>, int>> transitions, bool hasExitTime) : base(id, owner, canExecute, transitions, hasExitTime)
        {
        }

        public override void Execute()
        {
            base.Execute();
            movement.mode = MovementBase.Mode.Manual;
            animator.SetBool("doFall", true);
        }

        public override void Stop()
        {
            base.Stop();
            animator.SetBool("doFall", false);
        }

        public override int Update()
        {
            return base.Update();
        }
    }
}