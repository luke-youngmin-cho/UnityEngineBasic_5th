using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ULB.RPG.FSM
{
    public class EnemyStateAttack : CharacterStateBase
    {
        public EnemyStateAttack(int id, GameObject owner, Func<bool> canExecute, List<KeyValuePair<Func<bool>, int>> transitions, bool hasExitTime) : base(id, owner, canExecute, transitions, hasExitTime)
        {
        }
    }
}