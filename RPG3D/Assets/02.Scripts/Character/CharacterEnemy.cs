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
            _aiTree = new BehaviourTree();
            _aiTree.StartBuild()
                .Selector()
                    .InSight(this, 5.0f, 90, 1.0f, 0.75f, _targetMask)
                        .Logger("[CharacterEnemy] : Target Detected");

        }
    }
}