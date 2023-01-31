namespace ULB.RPG.AISystems
{
    public class Reverse : Decorator
    {
        public override Result Decorate(Result childResult, Behaviour leaf)
        {
            switch (childResult)
            {
                case Result.Success:
                    return Result.Failure;
                case Result.Failure:
                    return Result.Success;
                case Result.Running:
                    return Result.Running;
                default:
                    throw new System.Exception();
            }
        }

        public override Result Decorate(Result childResult, out Behaviour leaf)
        {
            throw new System.NotImplementedException();
        }
    }
}
