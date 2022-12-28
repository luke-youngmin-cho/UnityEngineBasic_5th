using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAssets : MonoBehaviour
{
    private static TowerAssets _instance;
    public static TowerAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<TowerAssets>("TowerAssets"));
            return _instance;
        }
    }

    [SerializeField] private List<Tower> _towers = new List<Tower>();
    [SerializeField] private List<GameObject> _previewTowers = new List<GameObject>();

    public IEnumerable<Tower> GetTowers(Predicate<Tower> match)
    {
        return _towers.FindAll(match);
    }

    public bool TryGetTower(TowerInfo info, out Tower tower)
    {
        tower = _towers.Find(t => t.Info.Equals(info));
        return tower;
    }

    public bool TryGetNextLevelTower(TowerInfo info, out Tower tower)
    {
        tower = _towers.Find(t => (t.Info.Type == info.Type) && (t.Info.UpgradeLevel == info.UpgradeLevel + 1));
        return tower;
    }

    public bool TryGetPreivewTower(TowerInfo info, out GameObject previewTower)
    {
        previewTower = _previewTowers.Find(t => t.name == info.name + "Preview");
        return previewTower;
    }
}
