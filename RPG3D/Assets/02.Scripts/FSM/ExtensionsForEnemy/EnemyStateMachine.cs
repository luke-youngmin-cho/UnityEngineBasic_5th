using System;
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
            Rigidbody rb = owner.GetComponent<Rigidbody>();
            GroundDetector groundDetector = owner.GetComponent<GroundDetector>();

            IState move = new EnemyStateMove(id: (int)StateType.Move,
                                             owner: owner,
                                             canExecute: () => true,
                                             transitions: new List<KeyValuePair<Func<bool>, int>>()
                                             {
                                                 new KeyValuePair<Func<bool>, int>
                                                 (
                                                     () => false,
                                                     (int)default(StateType)
                                                 ),
                                             },
                                             hasExitTime: false);
            states.Add(StateType.Move, move);

            IState attack = new EnemyStateAttack(id: (int)StateType.Attack,
                                              owner: owner,
                                              canExecute: () => currentType == StateType.Move,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => true,
                                                      (int)StateType.Move
                                                  ),
                                              },
                                              hasExitTime: false);
            states.Add(StateType.Attack, attack);

            IState hurt = new EnemyStateHurt(id: (int)StateType.Hurt,
                                              owner: owner,
                                              canExecute: () => true,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => true,
                                                      (int)StateType.Move
                                                  ),
                                              },
                                              hasExitTime: true);
            states.Add(StateType.Hurt, hurt);

            IState die = new EnemyStateDie(id: (int)StateType.Die,
                                              owner: owner,
                                              canExecute: () => true,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => false,
                                                      (int)StateType.Move
                                                  ),
                                              },
                                              hasExitTime: true);
            states.Add(StateType.Die, die);
        }
    }
}