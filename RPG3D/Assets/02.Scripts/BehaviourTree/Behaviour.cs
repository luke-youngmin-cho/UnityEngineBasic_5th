
namespace ULB.RPG.AISystems
{
    public enum Result
    {
        Success,
        Failure,
        Running
    }

    public abstract class Behaviour
    {
        public abstract Result Invoke(out Behaviour leaf);
    }

}
