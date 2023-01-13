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