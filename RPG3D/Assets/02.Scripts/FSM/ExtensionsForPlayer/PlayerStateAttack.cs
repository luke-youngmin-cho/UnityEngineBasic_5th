using System;
using System.Collections;
using System.Collections.Generic;
using ULB.RPG.InputSystems;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class PlayerStateAttack : CharacterStateBase
    {
        private enum Step
        {
            Idle,
            Prepare,
            Action,
            Combo,
        }
        private Step _step;

        public PlayerStateAttack(int id, GameObject owner, Func<bool> canExecute, List<KeyValuePair<Func<bool>, int>> transitions, bool hasExitTime) : base(id, owner, canExecute, transitions, hasExitTime)
        {
        }

        public override void Execute()
        {
            base.Execute();
            movement.mode = MovementBase.Mode.Manual;
            animator.SetBool("doAttack", true);
            _step = Step.Prepare;
        }

        public override void Stop()
        {
            base.Stop();
            animator.SetBool("doAttack", false);
        }

        public override int Update()
        {
            switch (_step)
            {
                case Step.Idle:
                    break;
                case Step.Prepare:
                    {
                        KeyInputHandler.instance.mouse0Trigger = false;
                        _step = Step.Action;
                    }
                    break;
                case Step.Action:
                    {
                        if (KeyInputHandler.instance.mouse0Trigger &&
                            animator.GetBool("finishCombo") == false)
                        {
                            animator.SetBool("doCombo", true);
                            _step = Step.Combo;
                        }
                        
                        if (animator.GetCurrentNormalizedTime(0) >= 0.98f)
                        {
                            return base.Update();
                        }
                    }
                    break;
                case Step.Combo:
                    {
                        if (animator.isPreviousStateFinished)
                        {
                            animator.SetBool("doCombo", false);
                            _step = Step.Prepare;
                        }
                    }
                    break;
                default:
                    break;
            }

            return id;
        }
    }
}