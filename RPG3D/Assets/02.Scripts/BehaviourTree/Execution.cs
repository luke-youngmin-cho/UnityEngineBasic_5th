using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULB.RPG.AISystems
{
    public class Execution : Behaviour
    {
        private Func<Result> _execute;


        public Execution(Func<Result> execute)
        {
            _execute = execute;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;
            return _execute.Invoke();
        }
    }
}
