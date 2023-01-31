using System;

namespace ULB.RPG.AISystems
{
    public class Condition : Behaviour, IChild
    {
        public Behaviour child { get; set; }
        private Func<bool> _condition;


        public Condition(Func<bool> condition)
        {
            _condition = condition;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            leaf = null;
            if (_condition.Invoke())
            {
                return child.Invoke(out leaf);
            }

            return Result.Failure;
        }
    }
}
