using ULB.RPG.FSM;

namespace ULB.RPG
{
    public class CharacterPlayer : CharacterBase
    {
        protected override CharacterStateMachine CreateMachine()
        {
            return new PlayerStateMachine(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();
            onHpDecreased += (value) => machine.ChangeState(CharacterStateMachine.StateType.Hurt);
            onHpMin += () => machine.ChangeState(CharacterStateMachine.StateType.Die);
        }

        public override void Hit()
        {
            base.Hit();
        }
    }
}