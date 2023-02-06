using UnityEngine;

namespace ULB.RPG
{
    internal class PlayerMovement : MovementBase
    {
        public override float v => Input.GetAxis("Vertical");
        public override float h => Input.GetAxis("Horizontal");
        public override float gain => Input.GetKey(KeyCode.LeftShift) ? 1.0f : 0.5f;
    }
}
