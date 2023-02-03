using UnityEngine;
using ULB.RPG.FSM;

namespace ULB.RPG
{
    /// <summary>
    /// FSM 을 동작시키는 캐릭터 베이스 클래스
    /// </summary>
    public abstract class CharacterBase : MonoBehaviour
    {
        public CharacterStateMachine machine;
        public Transform target;

        protected abstract CharacterStateMachine CreateMachine();
        protected virtual void UpdateMachine()
        {
            machine.Update();
        }

        protected virtual void Awake()
        {
            machine = CreateMachine();
        }

        protected virtual void Update()
        {
            UpdateMachine();
        }
    }
}
