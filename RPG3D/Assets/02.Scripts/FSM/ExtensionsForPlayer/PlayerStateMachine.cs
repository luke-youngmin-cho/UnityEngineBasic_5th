using System;
using System.Collections.Generic;
using ULB.RPG.InputSystems;
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
            Rigidbody rb = owner.GetComponent<Rigidbody>();
            GroundDetector groundDetector = owner.GetComponent<GroundDetector>();

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

            IState jump = new PlayerStateJump(id: (int)StateType.Jump,
                                              owner: owner,
                                              canExecute: () => groundDetector.isDetected,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => rb.velocity.y <= 0 && groundDetector.isDetected,
                                                      (int)StateType.Land
                                                  ),
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => rb.velocity.y <= 0,
                                                      (int)StateType.Fall
                                                  ),
                                              },
                                              hasExitTime: false);
            states.Add(StateType.Jump, jump);

            IState fall = new PlayerStateFall(id: (int)StateType.Fall,
                                              owner: owner,
                                              canExecute: () => true,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => groundDetector.isDetected,
                                                      (int)StateType.Land
                                                  ),
                                              },
                                              hasExitTime: false);
            states.Add(StateType.Fall, fall);

            IState land = new PlayerStateLand(id: (int)StateType.Land,
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
            states.Add(StateType.Land, land);

            KeyInputHandler.instance.RegisterKeyPressAction(KeyCode.Space, () => ChangeState(StateType.Jump));
        }
    }
}
