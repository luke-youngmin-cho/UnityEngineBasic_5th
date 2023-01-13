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
    }
}
