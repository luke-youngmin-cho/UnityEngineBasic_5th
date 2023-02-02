using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class EnemyStateMachine : CharacterStateMachine
    {
        public bool comboTrigger
        {
            get
            {
                if (_comboTrigger)
                {
                    _comboTrigger = false;
                    return true;
                }
                return false;
            }
            set
            {
                _comboTrigger = value;
            }
        }
        private bool _comboTrigger;

        public EnemyStateMachine(GameObject owner) : base(owner)
        {
        }

        public override void InitStates()
        {
            throw new System.NotImplementedException();
        }
    }
}