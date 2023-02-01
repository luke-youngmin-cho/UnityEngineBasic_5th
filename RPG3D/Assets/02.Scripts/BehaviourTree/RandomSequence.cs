using System;
using System.Linq;

namespace ULB.RPG.AISystems
{
    public class RandomSequence : Composite
    {
        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;
            Result result;
            foreach (var child in children.OrderBy(c => Guid.NewGuid()))
            {
                result = child.Invoke(out leaf);
                if (result != Result.Success)
                    return result;
            }

            return Result.Success;
        }
    }
}
