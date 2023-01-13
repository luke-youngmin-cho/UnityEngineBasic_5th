using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class PlayerStateMachine : CharacterStateMachine
    {
        public PlayerStateMachine(GameObject owner) : base(owner)
        {
        }

        public override void InitStates()
        {
            IState move = new PlayerStateMove(id: (int)StateType.Move,
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
        }
    }
}
