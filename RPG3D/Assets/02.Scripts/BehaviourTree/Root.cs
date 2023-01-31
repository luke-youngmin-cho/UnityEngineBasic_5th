using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULB.RPG.AISystems
{
    public class Root : Behaviour, IChild
    {        
        public Behaviour child { get; set; }
        private Behaviour _running;

        public Result Invoke()
        {
            Result result;
            Behaviour leaf;

            // running 을 반환했던 leaf behaviour 가 있으면 그걸 실행.
            if (_running == null)
            {
                result = Invoke(out leaf);
            }
            else
            {
                result = _running.Invoke(out leaf);
            }

            // running 이 반환되면 leaf behaviour 저장.
            if (result == Result.Running)
            {
                _running = leaf;
            }
            else
            {
                _running = null;
            }

            return result;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            return child.Invoke(out leaf);
        }
    }
}
