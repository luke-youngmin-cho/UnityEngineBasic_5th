using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public static TowerUI Instance;

    [SerializeField] private Button _upgrade;
    [SerializeField] private Button _sell;
    [SerializeField] private TMP_Text _upgradePrice;
    [SerializeField] private TMP_Text _sellPrice;
    [SerializeField] private Vector3 _offset;
    private int _nextLevelTowerBuildPrice;

    public void SetUp(Tower tower)
    {
        if (TowerHandler.Instance.IsActivated)
            return;

        // upgrade button
        if (TowerAssets.Instance.TryGetNextLevelTower(tower.Info, out Tower next))
        {
            _upgrade.gameObject.SetActive(true);
            _nextLevelTowerBuildPrice = next.Info.BuildPrice;
            _upgradePrice.text = _nextLevelTowerBuildPrice.ToString();

            RefreshUpgradePriceColor();

            _upgrade.onClick.RemoveAllListeners();
            _upgrade.onClick.AddListener(() =>
            {
                if (_nextLevelTowerBuildPrice > Player.Instance.Money)
                    return;

                Player.Instance.Money -= next.Info.BuildPrice;
                SetUp(tower.Node.BuildTowerHere(next));
            });
        }
        else
        {
            _upgrade.gameObject.SetActive(false);
        }

        // sell button
        _sellPrice.text = tower.Info.SellPrice.ToString();
        _sell.onClick.RemoveAllListeners();
        _sell.onClick.AddListener(() =>
        {
            Player.Instance.Money += tower.Info.SellPrice;
            Destroy(tower.gameObject);
            gameObject.SetActive(false);
        });

        // position
        transform.position = tower.transform.position + _offset;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        Player.Instance.OnMoneyChanged += RefreshUpgradeColor;
        gameObject.SetActive(false);
    }

    private void RefreshUpgradeColor(int index)
    {
        if (_nextLevelTowerBuildPrice > Player.Instance.Money)
        {
            _upgradePrice.color = Color.red;
        }
        else
        {
            _upgradePrice.color = Color.black;
        }
    }

    private void RefreshUpgradePriceColor()
    {
        if (_nextLevelTowerBuildPrice > Player.Instance.Money)
        {
            _upgradePrice.color = Color.red;
        }
        else
        {
            _upgradePrice.color = Color.black;
        }
    }
}
