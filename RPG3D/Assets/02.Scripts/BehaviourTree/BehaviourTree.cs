
namespace ULB.RPG.AISystems
{
    public class BehaviourTree
    {
        private Root _root;

        public void Tick()
        {
            _root.Invoke();
        }
    }
}