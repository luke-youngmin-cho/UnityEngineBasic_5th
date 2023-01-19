using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    /// <summary>
    /// 캐릭터 상태를 수행하는 머신
    /// </summary>
    public abstract class CharacterStateMachine
    {
        public enum StateType
        {
            Move,
            Jump,
            Fall,
            Land,
            Attack,
            Hurt,
            Die,
        }

        public StateType currentType;
        public IState current;
        protected Dictionary<StateType, IState> states;
        protected GameObject owner;

        public CharacterStateMachine(GameObject owner)
        {
            this.owner = owner;
            states = new Dictionary<StateType, IState>();

            InitStates();

            currentType = default(StateType);
            current = states[currentType];
            current.Execute();
        }

        /// <summary>
        /// 상태들을 만들고 초기화하는 함수.
        /// </summary>
        public abstract void InitStates();

        /// <summary>
        /// 상태 전환 가능한지 체크하고 전환하는 함수
        /// </summary>
        /// <param name="nextType"> 전환 하고자하는 다음 상태 타입</param>
        /// <returns> 전환 성공 여부 </returns>
        public bool ChangeState(StateType nextType)
        {
            if (currentType == nextType)
                return false;

            if (states[nextType].canExecute.Invoke())
            {
                Debug.Log($"{currentType} -> {nextType}");
                current.Stop();
                current = states[nextType];
                current.Execute();
                currentType = nextType;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 현재 상태의 로직을 수행하는 함수
        /// </summary>
        public void Update()
        {
            ChangeState((StateType)current.Update());
        }
    }
}
