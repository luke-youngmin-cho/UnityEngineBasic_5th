
namespace ULB.RPG.AISystems
{
    public abstract class Decorator : Behaviour, IChild
    {
        public Behaviour child { get; set; }


        public override Result Invoke(out Behaviour leaf)
        {
            return Decorate(child.Invoke(out leaf), leaf);
        }

        public abstract Result Decorate(Result childResult, out Behaviour leaf);
        public abstract Result Decorate(Result childResult, Behaviour leaf);
    }
}
