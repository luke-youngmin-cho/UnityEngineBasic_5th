using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.StatSystems
{
    [Serializable]
    public class Stats
    {
        public List<Stat> stats = new List<Stat>();
        public Stat this[StatType stateType] => stats.Find(x => x.type == stateType);
    }
}