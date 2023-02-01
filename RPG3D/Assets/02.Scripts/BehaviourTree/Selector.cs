namespace ULB.RPG.AISystems
{
    public class Selector : Composite
    {
        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;
            Result result;
            foreach (var child in children)
            {
                result = child.Invoke(out leaf);    
                if (result != Result.Failure)
                    return result;
            }

            return Result.Failure;
        }
    }
}
