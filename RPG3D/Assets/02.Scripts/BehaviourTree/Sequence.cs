namespace ULB.RPG.AISystems
{
    public class Sequence : Composite
    {
        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;
            Result result;
            foreach (var child in children)
            {
                result = child.Invoke(out leaf);
                if (result != Result.Success)
                    return result;
            }

            return Result.Success;
        }
    }
}
