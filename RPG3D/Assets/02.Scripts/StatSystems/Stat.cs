using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.StatSystems
{
    [Serializable]
    public class Stat
    {
        public StatType type;

        // 고유 스텟값 (레벨업 시 획득한 스텟포인트로 증가시킬 수 있는 값)
        public int value
        {
            get
            {
                return _value;
            }
            set 
            {
                _value = value;
                onValueChanged?.Invoke(value);
            }
        }
        private int _value;
        public event Action<int> onValueChanged;
        // 조정된 스텟값 (장비 장착/ 버프 획득 등으로 인해 조정받은 스텟 값)
        public int valueModified
        {
            get
            {
                return _valueModified;
            }
            set
            {
                _valueModified = value;
                onValueModifiedChanged?.Invoke(value);
            }
        }
        private int _valueModified;
        public event Action<int> onValueModifiedChanged;
        public List<StatModifier> modifiers = new List<StatModifier>();


        //==============================================================================
        //                               Public Methods
        //==============================================================================

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);
            valueModified = CalcValueModified();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            modifiers.Remove(modifier);
            valueModified = CalcValueModified();
        }

        public int CalcValueModified()
        {
            int sumFlatAdd = 0;
            float sumPercentAdd = 0;
            float sumPercentMul = 1.0f;

            foreach (StatModifier modifier in modifiers)
            {
                switch (modifier.modType)
                {
                    case StatModType.FlatAdd:
                        {
                            sumFlatAdd += modifier.value;
                        }
                        break;
                    case StatModType.PercentAdd:
                        {
                            sumPercentAdd += (modifier.value / 100.0f);
                        }
                        break;
                    case StatModType.PercentMul:
                        {
                            sumPercentMul *= (modifier.value / 100.0f);
                        }
                        break;
                    default:
                        break;
                }
            }

            return Mathf.RoundToInt((value + sumFlatAdd) * sumPercentMul * sumPercentMul);
        }
    }
}