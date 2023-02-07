using System;

namespace ULB.RPG
{
    public interface IDamageable
    {
        int hp { get; set; }
        int hpMax { get; }
        event Action<int> onHpDecreased;
        event Action<int> onHpIncreased;
        event Action onHpMin;
        event Action onHpMax;

        void Damage(int amount);
        void Heal(int amout);
    }
}