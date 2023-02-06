using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG
{
    public class EnemyMovement : MovementBase
    {
        public override float v => _v;
        public override float h => _h;
        public override float gain => _gain;

        private float _v, _h, _gain;
        public override void SetMove(float v, float h, float gain)
        {
            base.SetMove(v, h, gain);
            _v = v;
            _h = h;
            _gain = gain;
        }

    }
}