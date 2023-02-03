using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG
{
    public class EnemyMovement : MovementBase
    {
        protected override float v { get; }

        protected override float h { get; }

        protected override float gain { get; }
    }
}