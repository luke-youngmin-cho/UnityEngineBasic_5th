
using UnityEngine;

namespace ULB.RPG.AISystems
{
    public class BehaviourTreeTester : MonoBehaviour
    {
        private BehaviourTree _tree;

        private void Start()
        {
            _tree = new BehaviourTree();
            _tree.root = new Root();
            _tree.root.child = new Sequence();
            ((Sequence)_tree.root.child)
                .children.Add(new Parallel(2, 0));
            ((Parallel)((Sequence)_tree.root.child)
                .children[0])
                .children.Add(new Condition(() => true));
            ((Condition)((Parallel)((Sequence)_tree.root.child)
                .children[0])
                .children[0]).child = new Execution(() => Result.Success);
            ((Parallel)((Sequence)_tree.root.child)
                .children[1])
                .children.Add(new Condition(() => true));
            ((Condition)((Parallel)((Sequence)_tree.root.child)
                .children[0])
                .children[0]).child = new Execution(() => Result.Success);
            ((Parallel)((Sequence)_tree.root.child)
                .children[2])
                .children.Add(new Condition(() => true));
            ((Condition)((Parallel)((Sequence)_tree.root.child)
                .children[0])
                .children[0]).child = new Execution(() => Result.Success);
            ((Sequence)_tree.root.child)
                .children.Add(new Execution(() => Result.Success));

            _tree.StartBuild()
                .Sequence()
                    .Parallel(2, 0)
                        .Condition(() => true)
                            .Execution(() => Result.Success)
                        .Condition(() => true)
                            .Execution(() => Result.Success)
                        .Condition(() => true)
                            .Execution(() => Result.Success)
                        .ExitCurrentComposite()
                    .Execution(() => Result.Success);
        }
    }
}
