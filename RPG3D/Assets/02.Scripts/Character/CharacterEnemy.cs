using System.Collections;
using System.Collections.Generic;
using ULB.RPG.AISystems;
using ULB.RPG.FSM;
using UnityEngine;

namespace ULB.RPG
{
    public class CharacterEnemy : CharacterBase
    {
        private BehaviourTree _aiTree;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private float _attackRange = 1.0f;

        protected override void Awake()
        {
            base.Awake();
            BuildAITree();
        }

        protected override void Update()
        {
            _aiTree.Tick();
            base.Update();
        }

        protected override CharacterStateMachine CreateMachine()
        {
            return new EnemyStateMachine(this.gameObject);
        }


        private void BuildAITree()
        {
            EnemyMovement movement = GetComponent<EnemyMovement>();

            _aiTree = new BehaviourTree();
            _aiTree.StartBuild()
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
                                                                            new Vector3(Random.Range(0.1f, 0.5f), 0.0f, Random.Range(0.1f, 0.9f)),
                                                                            Random.Range(0.0f, 0.5f));
                                        movement.SetMove(interpolated.x, interpolated.z, Random.Range(0.5f, 1.0f));
                                    }
                                    else
                                    {
                                        movement.SetMove(1.0f, 0.0f, 1.0f);
                                    }

                                    transform.LookAt(target);
                                    return Result.Success;
                                }
                                else
                                {
                                    return Result.Failure;
                                }
                            });

        }
    }
}