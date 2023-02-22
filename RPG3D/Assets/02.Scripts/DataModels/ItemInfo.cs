using System.Collections;
using System.Collections.Generic;
using ULB.RPG;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemInfo", menuName = "RPG/Create ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public int id;
    public string description;
    public int maxNum;
    public Sprite icon;
    public Item prefab;
}
