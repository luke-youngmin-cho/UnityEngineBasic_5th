using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    /// <summary>
    /// 애니메이터를 사용하는 캐릭터 FSM 의 상태 베이스
    /// </summary>
    public abstract class CharacterStateBase : IState
    {
        protected GameObject owner;
        public int id { get; set; }
        public Func<bool> canExecute { get; set; }
        public List<KeyValuePair<Func<bool>, int>> transitions { get; set; }

        protected MovementBase movement;
        protected AnimatorWrapper animator;
        protected bool hasExitTime;

        public CharacterStateBase(int id, 
                                  GameObject owner, 
                                  Func<bool> canExecute,
                                  List<KeyValuePair<Func<bool>, int>> transitions, 
                                  bool hasExitTime)
        {
            this.id = id;
            this.owner = owner;
            this.canExecute = canExecute;            
            this.transitions = transitions;
            this.hasExitTime = hasExitTime;
            animator = owner.GetComponent<AnimatorWrapper>();
            movement = owner.GetComponent<MovementBase>();

            // * 주의 
            // 반환값이 있는 대리자에 여러 함수를 등록할 경우 
            // 마지막에 등록된 함수의 반환값을 최종적으로 반환함.
            this.canExecute = () =>
            {   
                return canExecute.Invoke() && 
                       animator.isPreviousMachineFinished && 
                       animator.isPreviousStateFinished;
            };
        }

        public virtual void Execute()
        {
        }

        public virtual void Stop()
        {
        }

        public virtual int Update()
        {
            int nextID = id;

            if (hasExitTime == false ||
                (hasExitTime == true && animator.GetCurrentNormalizedTime() >= 1.0f))
            {
                foreach (var transition in transitions)
                {
                    if (transition.Key.Invoke())
                    {
                        nextID = transition.Value;
                        break;
                    }
                }
            }

            return nextID;
        }
    }
}