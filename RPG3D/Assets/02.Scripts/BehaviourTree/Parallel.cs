
namespace ULB.RPG.AISystems
{
    public class Parallel : Composite
    {
        private int _successPolicy;
        private int _failurePolicy; 

        public Parallel(int successPolicy, int failurePolicy)
        {
            _successPolicy = successPolicy;
            _failurePolicy = failurePolicy;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;
            int successCount = 0;
            int failureCount = 0;
            Result result;
            foreach (var child in children)
            {
                result = child.Invoke(out leaf);
                switch (result)
                {
                    case Result.Success:
                        successCount++;
                        break;
                    case Result.Failure:
                        failureCount++;
                        break;
                    default:
                        break;
                }
            }

            if (successCount >= _successPolicy)
                return Result.Success;
            else if (failureCount >= _failurePolicy)
                return Result.Failure;
            else
                return Result.Running;
        }
    }
}
