using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ULB.RPG.FSM
{

    public class PlayerStateJump : CharacterStateBase
    {
        private Rigidbody _rb;
        private float _jumpForce = 4.0f;
        private float _startTimeMark;

        public PlayerStateJump(int id, GameObject owner, Func<bool> canExecute, List<KeyValuePair<Func<bool>, int>> transitions, bool hasExitTime) : base(id, owner, canExecute, transitions, hasExitTime)
        {
            _rb = owner.GetComponent<Rigidbody>();
        }

        public override void Execute()
        {
            base.Execute();
            movement.mode = MovementBase.Mode.Manual;
            animator.SetBool("doJump", true);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _startTimeMark = Time.time;
        }

        public override void Stop()
        {
            base.Stop();
            animator.SetBool("doJump", false);
        }

        public override int Update()
        {
            return (Time.time - _startTimeMark > 0.01f) ? base.Update() : id;
        }
    }
}