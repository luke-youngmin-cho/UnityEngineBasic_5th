using System.Collections.Generic;
using UnityEngine;
public class LevelDataAssets : MonoBehaviour
{
    public static LevelDataAssets _instance;
    public static LevelDataAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<LevelDataAssets>("LevelDataAssets"));
            return _instance;
        }
    }

    [SerializeField] private List<LevelData> _levelDatas = new List<LevelData>();
    private Dictionary<int, LevelData> _levelDataDictionary = new Dictionary<int, LevelData>();

    private void Awake()
    {
        foreach (LevelData levelData in _levelDatas)
        {
            _levelDataDictionary.Add(levelData.Level, levelData);
        }
    }

    public bool TryGetLevelData(int level, out LevelData data)
    {
        return _levelDataDictionary.TryGetValue(level, out data);
    }

}