using UnityEngine;

namespace ULB.RPG.AISystems
{
    public class Logger : Behaviour
    {
        private string _log;

        public Logger(string log)
        {
            _log = log;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;
            Debug.Log(_log);
            return Result.Success;
        }
    }
}
