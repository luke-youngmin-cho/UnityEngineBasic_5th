using System.Collections;
using System.Collections.Generic;
using ULB.RPG;
using UnityEngine;
using UnityEngine.UI;

public class Test_EquipButtons : MonoBehaviour
{
    public CharacterPlayer target;
    public Equipment equipment;
    [SerializeField] private Button _equip;
    [SerializeField] private Button _Unequip;


    private void Awake()
    {
        _equip.onClick.AddListener(() =>
        {
            target.TryEquip(equipment);
        });

        _Unequip.onClick.AddListener(() =>
        {
            target.TryUnequip(EquipType.DoubleHandWeapon);
        });
    }

}
