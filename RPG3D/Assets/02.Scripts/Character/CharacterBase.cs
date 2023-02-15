using UnityEngine;
using ULB.RPG.FSM;
using System;
using ULB.RPG.StatSystems;

namespace ULB.RPG
{
    /// <summary>
    /// FSM 을 동작시키는 캐릭터 베이스 클래스
    /// </summary>
    public abstract class CharacterBase : MonoBehaviour, IDamageable
    {
        public CharacterStateMachine machine;
        public Transform target;

        public Stats stats;
        public event Action<int> onHpDecreased;
        public event Action<int> onHpIncreased;
        public event Action onHpMin;
        public event Action onHpMax;

        public int hp
        {
            get
            {
                return _hp;
            }
            set
            {
                // Heal
                if (_hp < value)
                {
                    _hp = value;

                    if (value < hpMax)
                        onHpIncreased?.Invoke(value);
                    else
                        onHpMax?.Invoke();
                }
                // Damage
                else if (_hp > value)
                {
                    _hp = value;

                    if (value > 0)
                        onHpDecreased?.Invoke(value);
                    else
                        onHpMin?.Invoke();
                }

            }
        }

        public int hpMax => _hpMax;

        private int _hp;
        [SerializeField] private int _hpMax;

        protected abstract CharacterStateMachine CreateMachine();
        protected virtual void UpdateMachine()
        {
            machine.Update();
        }

        protected virtual void Awake()
        {
            hp = hpMax;
            machine = CreateMachine();
        }

        protected virtual void Update()
        {
            UpdateMachine();
        }


        public virtual void FootR() { }
        public virtual void FootL() { }
        public virtual void Land() { }
        public virtual void Hit() { }

        public void Damage(int amount)
        {
            hp -= amount;
        }

        public void Heal(int amout)
        {
            hp += amout;
        }
    }
}
