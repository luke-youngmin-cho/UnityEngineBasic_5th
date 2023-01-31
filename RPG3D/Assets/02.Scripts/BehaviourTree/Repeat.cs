

namespace ULB.RPG.AISystems
{
    public class Repeat : Decorator
    {
        public enum Policy
        {
            DoesntMatter,
            OnlySuccess,
            OnlyFailure,
        }
        private Policy _policy;
        private int _times;
        private int _count;

        public Repeat(int times, Policy repeatPolicy)
        {
            _times = times;
            _policy = repeatPolicy;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            return Decorate(Result.Running, out leaf);
        }

        public override Result Decorate(Result childResult, out Behaviour leaf)
        {
            _count--;

            if (_count <= 0)
            {
                _count = _times;
                return child.Invoke(out leaf);
            }
            else
            {
                Result tmpResult = child.Invoke(out leaf);

                switch (_policy)
                {
                    case Policy.DoesntMatter:
                        break;
                    case Policy.OnlySuccess:
                        if (tmpResult != Result.Success)
                        {
                            return tmpResult;
                        }
                        break;
                    case Policy.OnlyFailure:
                        if (tmpResult != Result.Failure)
                        {
                            return tmpResult;
                        }
                        break;
                    default:
                        break;
                }

                leaf = this;
                return Result.Running;
            }
        }

        public override Result Decorate(Result childResult, Behaviour leaf)
        {
            throw new System.NotImplementedException();
        }
    }
}
