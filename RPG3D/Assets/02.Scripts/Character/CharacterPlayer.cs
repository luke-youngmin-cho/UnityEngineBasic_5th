using ULB.RPG.FSM;

namespace ULB.RPG
{

    public class CharacterPlayer : CharacterBase
    {
        protected override CharacterStateMachine CreateMachine()
        {
            return new PlayerStateMachine(gameObject);
        }
    }
}