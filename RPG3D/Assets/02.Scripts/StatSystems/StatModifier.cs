using System;
using System.Collections;
using UnityEngine;

namespace ULB.RPG.StatSystems
{
    [Serializable]
    public class StatModifier
    {
        public StatType type;
        public int value;
        public StatModType modType;

        public StatModifier(StatType type, int value, StatModType modType)
        {
            this.type = type;
            this.value = value;
            this.modType = modType;
        }
    }
}