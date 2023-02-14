using System.Collections;
using System.Collections.Generic;
using ULB.RPG.AISystems;
using ULB.RPG.FSM;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace ULB.RPG
{
    public class CharacterEnemy : CharacterBase
    {
        private BehaviourTree _aiTree;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private float _attackRange = 1.0f;

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

        protected override void Awake()
        {
            base.Awake();
            BuildAITree();
            onHpDecreased += (value) =>
            {
                _aiTree.Reset();
                machine.ChangeState(CharacterStateMachine.StateType.Hurt);
                Debug.Log($"{gameObject.name} hurt {value}");
            };
            onHpMin += () =>
            {
                _aiTree.Reset();
                machine.ChangeState(CharacterStateMachine.StateType.Die);
                Debug.Log($"{gameObject.name} die");
            };
        }

        protected override void Update()
        {
            _aiTree.Tick();
            base.Update();
        }

        protected override CharacterStateMachine CreateMachine()
        {
            machine = new EnemyStateMachine(this.gameObject);
            return machine;
        }


        private void BuildAITree()
        {
            EnemyMovement movement = GetComponent<EnemyMovement>();

            _aiTree = new BehaviourTree();
            _aiTree.StartBuild()
                .Sequence()
                    .Condition(() =>
                    {
                        return machine.currentType != CharacterStateMachine.StateType.Hurt &&
                               machine.currentType != CharacterStateMachine.StateType.Die;
                    })
                        .Execution(() => Result.Success)
                    .Selector()
                        .InSight(this, 10.0f, 90, 1.0f, 0.75f, _targetMask)
                            .Selector()
                                .Condition(() => Vector3.Distance(transform.position, target.position) < _attackRange)
                                    .Execution(() =>
                                {
                                    if (machine.ChangeState(CharacterStateMachine.StateType.Attack) ||
                                        machine.currentType == CharacterStateMachine.StateType.Attack)
                                    {
                                        return Result.Success;
                                    }
                                    else
                                    {
                                        return Result.Failure;
                                    }
                                })
                                .Execution(() =>
                            {
                                if (machine.ChangeState(CharacterStateMachine.StateType.Move) ||
                                    machine.currentType == CharacterStateMachine.StateType.Move)
                                {
                                    if (Vector3.Distance(transform.position, target.position) > 7.0f)
                                    {
                                        Vector3 interpolated = Vector3.Lerp(new Vector3(movement.h, 0.0f, movement.v),
                                                                            new Vector3(Random.Range(0.1f, 0.5f), 0.0f, Random.Range(0.01f, 0.3f)),
                                                                            Random.Range(0.0f, 0.5f));
                                        movement.SetMove(interpolated.x, interpolated.z, Random.Range(0.5f, 1.0f));
                                    }
                                    else if (Vector3.Distance(transform.position, target.position) > 1.0f)
                                    {
                                        movement.SetMove(1.0f, 0.0f, 1.0f);
                                    }
                                    else
                                    {
                                        movement.SetMove(0.1f, 0.1f, 0.0f);
                                    }

                                    transform.LookAt(target);
                                    return Result.Success;
                                }
                                else
                                {
                                    return Result.Failure;
                                }
                            })
                                .ExitCurrentComposite()
                        .RandomSelector()
                            .RandomKeep(1.0f, 3.0f)
                                .Execution(() =>
                            {
                                if (machine.ChangeState(CharacterStateMachine.StateType.Move) ||
                                    machine.currentType == CharacterStateMachine.StateType.Move)
                                {
                                    movement.SetMove(0.0f, 0.0f, 0.0f);
                                    return Result.Success;
                                }
                                else
                                {
                                    return Result.Failure;
                                }
                            })
                            .RandomSleep(1.0f, 3.0f)
                                .Execution(() =>
                            {
                                if (machine.ChangeState(CharacterStateMachine.StateType.Move) ||
                                    machine.currentType == CharacterStateMachine.StateType.Move)
                                {
                                    transform.Rotate(Vector3.up * Random.Range(0.0f, 360.0f));
                                    movement.SetMove(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), 1.0f);
                                    return Result.Success;
                                }
                                else
                                {
                                    return Result.Failure;
                                }
                            });

        }

        #region AnimationEvents
        public void StartCastRightHandWeapon() { }
        public void HitRightHandWeapon(int targetNum) { }
        public void StartCastLeftHandWeapon() { }
        public void HitLeftHandWeapon(int targetNum) { }


        #endregion
    }
}