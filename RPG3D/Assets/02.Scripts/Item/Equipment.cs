using System.Collections;
using UnityEngine;


public abstract class Equipment : Item
{
    public enum EquipType
    {
        RightHandWeapon,
        LeftHandWeapon,
        DoubleHandWeapon,
        Top,
        Bottom,
        Head,
        Ring,
        Necklace
    }
    public EquipType type;
    public virtual void Equip() { }
    public virtual void Unequip() { }
}
