using System;
using System.Collections.Generic;
using ULB.RPG.InputSystems;
using UnityEditor.VersionControl;
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
                                              canExecute: () => groundDetector.isDetected &&
                                                                currentType == StateType.Move,
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
                                                      () => groundDetector.CastGround(0.5f, out RaycastHit hit) ||
                                                            groundDetector.isDetected,
                                                      (int)StateType.Land
                                                  ),
                                              },
                                              hasExitTime: false);
            states.Add(StateType.Fall, fall);

            IState land = new PlayerStateLand(id: (int)StateType.Land,
                                              owner: owner,
                                              canExecute: () => currentType == StateType.Jump ||
                                                                currentType == StateType.Fall,
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

            IState attack = new PlayerStateAttack(id: (int)StateType.Attack,
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

            IState hurt = new PlayerStateHurt(id: (int)StateType.Hurt,
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

            IState die = new PlayerStateDie(id: (int)StateType.Die,
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

            KeyInputHandler.instance.RegisterKeyPressAction(KeyCode.Space, () => ChangeState(StateType.Jump));
            KeyInputHandler.instance.OnMouse0TriggerActivated += () => ChangeState(StateType.Attack);

            KeyInputHandler.instance.RegisterKeyDownAction(KeyCode.H, () => ChangeState(StateType.Hurt));
            KeyInputHandler.instance.RegisterKeyDownAction(KeyCode.K, () => ChangeState(StateType.Die));
        }
    }
}
