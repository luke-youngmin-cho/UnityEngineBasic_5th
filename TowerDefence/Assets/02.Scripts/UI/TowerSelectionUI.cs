using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionUI : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private Button _buttonPrefab;

    private void Start()
    {
        Button button = null;
        foreach (Tower tower in TowerAssets.Instance.GetTowers(t => t.Info.UpgradeLevel == 1))
        {
            button = Instantiate(_buttonPrefab, _content);
            button.onClick.AddListener(() => TowerHandler.Instance.Handle(tower.Info));
            button.GetComponent<Image>().sprite = tower.Info.Icon;
        } 
    }
}
