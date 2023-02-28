using System.Collections;
using System.Collections.Generic;
using ULB.RPG.StatSystems;
using UnityEngine;

namespace ULB.RPG
{
    public abstract class Equipment : Item
    {
        public EquipType type;
        public List<StatModifier> statModifiers = new List<StatModifier>();

        public virtual void Equip(CharacterBase subject) 
        {
            foreach (StatModifier statModifier in statModifiers)
            {
                subject.stats[statModifier.type].AddModifier(statModifier);
            }
        }
        public virtual void Unequip(CharacterBase subject) 
        {
            foreach (StatModifier statModifier in statModifiers)
            {
                subject.stats[statModifier.type].RemoveModifier(statModifier);
            }
        }
    }
}