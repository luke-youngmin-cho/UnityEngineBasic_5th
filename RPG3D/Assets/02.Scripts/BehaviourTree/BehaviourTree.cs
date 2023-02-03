
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.AISystems
{
    public class BehaviourTree
    {
        private Root _root;
        public Root root
        {
            get => _root;
            set => _root = value;
        }

        public void Tick()
        {
            _root.Invoke();
        }

        #region Builder
        private Behaviour _current;
        private Stack<Composite> _compositeStack = new Stack<Composite>();

        public BehaviourTree StartBuild()
        {
            _root = new Root();
            _current = root;
            return this;
        }

        public void AttachAsChild(Behaviour parent, Behaviour child)
        {
            if (parent is Composite)
            {
                ((Composite)parent).children.Add(child);
            }
            else if (parent is IChild)
            {
                ((IChild)parent).child = child;
            }
            else
            {
                throw new System.Exception("[BehaviourTree] : 자식이 없는 행동에 자식을 추가하려고 시도했습니다.");
            }
        }

        public BehaviourTree ExitCurrentComposite()
        {
            if (_compositeStack.Count > 1)
            {
                _compositeStack.Pop();
                _current = _compositeStack.Peek();
            }
            else
            {
                _current = null;
            }

            return this;
        }

        public BehaviourTree Sequence()
        {
            Composite sequence = new Sequence();
            _compositeStack.Push(sequence);
            AttachAsChild(_current, sequence);
            _current = sequence;
            return this;
        }
        public BehaviourTree RandomSequence()
        {
            Composite sequence = new RandomSequence();
            _compositeStack.Push(sequence);
            AttachAsChild(_current, sequence);
            _current = sequence;
            return this;
        }

        public BehaviourTree Selector()
        {
            Composite selector = new Selector();
            _compositeStack.Push(selector);
            AttachAsChild(_current, selector);
            _current = selector;
            return this;
        }

        public BehaviourTree RandomSelector()
        {
            Composite selector = new RandomSelector();
            _compositeStack.Push(selector);
            AttachAsChild(_current, selector);
            _current = selector;
            return this;
        }

        public BehaviourTree Parallel(int successPolicy, int failurePolicy)
        {
            Composite parallel = new Parallel(successPolicy, failurePolicy);
            _compositeStack.Push(parallel);
            AttachAsChild(_current, parallel);
            _current = parallel;
            return this;
        }

        public BehaviourTree Condition(Func<bool> func)
        {
            Behaviour condition = new Condition(func);
            AttachAsChild(_current, condition);
            _current = condition;
            return this;
        }

        public BehaviourTree Execution(Func<Result> execute)
        {
            Behaviour execution = new Execution(execute);
            AttachAsChild(_current, execution);

            if (_compositeStack.Count > 0)
                _current = _compositeStack.Peek();
            else
                _current = null;

            return this;
        }

        public BehaviourTree Repeat(int times, Repeat.Policy repeatPolicy)
        {
            Behaviour repeat = new Repeat(times, repeatPolicy);
            AttachAsChild(_current, repeat);
            _current = repeat;
            return this;
        }

        public BehaviourTree InSight(CharacterBase owner, float radius, float angle, float angleDelta, float height, LayerMask targetMask)
        {
            Behaviour inSight = new InSight(owner, radius, angle, angleDelta, height, targetMask);
            AttachAsChild(_current, inSight);
            _current = inSight;
            return this;
        }

        public BehaviourTree Logger(string log)
        {
            Behaviour logger = new Logger(log);
            AttachAsChild(_current, logger);

            if (_compositeStack.Count > 0)
                _current = _compositeStack.Peek();
            else
                _current = null;

            return this;
        }
        #endregion
    }
}