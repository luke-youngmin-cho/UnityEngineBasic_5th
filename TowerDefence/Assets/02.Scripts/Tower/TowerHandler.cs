using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    public static TowerHandler instance;

    private GameObject _previewTower;
    private TowerInfo _info;
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private LayerMask _nodeLayer;

    public void Handle(TowerInfo info)
    {
        if (_previewTower != null)
            Destroy(_previewTower);

        if (TowerAssets.Instance.TryGetPreivewTower(info, out GameObject previewTowerPrefab))
        {
            _info = info;
            _previewTower = Instantiate(previewTowerPrefab);
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[TowerHandler] : {info.name} �� �̸����� Ÿ���� ������ �� �����ϴ�. �̸��� Ȯ���ϼ���");
        }
    }

    public void Cancel()
    {
        if (_previewTower != null)
            Destroy(_previewTower);

        gameObject.SetActive(false);
        _info = null;
    }
}
