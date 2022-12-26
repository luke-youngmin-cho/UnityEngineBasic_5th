using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    public static TowerHandler Instance;

    public bool IsActivated;
    [SerializeField] private MeshFilter _meshFilterBase;
    [SerializeField] private MeshRenderer _meshRendererBase;
    [SerializeField] private MeshFilter _meshFilterTurret;
    [SerializeField] private MeshRenderer _meshRendererTurret;
    private TowerInfo _info;
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private LayerMask _nodeLayer;

    public void Handle(TowerInfo info)
    {
        if (TowerAssets.Instance.TryGetPreivewTower(info, out GameObject previewTowerPrefab))
        {
            _info = info;
            _meshFilterBase.mesh = previewTowerPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
            _meshRendererBase.material = previewTowerPrefab.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
            _meshFilterBase.transform.localPosition = previewTowerPrefab.transform.GetChild(0).localPosition;
            _meshFilterTurret.mesh = previewTowerPrefab.transform.GetChild(1).GetComponent<MeshFilter>().sharedMesh;
            _meshRendererTurret.material = previewTowerPrefab.transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial;
            _meshFilterTurret.transform.localPosition = previewTowerPrefab.transform.GetChild(1).localPosition;
            IsActivated = true;
        }
        else
        {
            Debug.LogWarning($"[TowerHandler] : {info.name} 의 미리보기 타워를 가져올 수 없습니다. 이름을 확인하세여");
        }
    }

    public void Cancel()
    {
        _info = null;
        _meshFilterBase.mesh = null;
        _meshRendererBase.material = null;
        _meshFilterTurret.mesh = null;
        _meshRendererTurret.material = null;
        IsActivated = false;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsActivated == false)
            return;

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _nodeLayer))
        {
            transform.position = new Vector3(_hit.collider.transform.position.x,
                                             _hit.point.y,
                                             _hit.collider.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(5000.0f, 5000.0f, 5000.0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetMouseButtonDown(1))
            Cancel();

        if (Input.GetMouseButtonUp(0))
            OnClick();
    }

    private void OnClick()
    {
        if (Player.Instance.Money < _info.BuildPrice)
        {
            Debug.Log("잔액이 부족합니다.");
            return;
        }

        if (_hit.collider == null)
        {
            Debug.Log("건설할 위치가 올바르지 않습니다");
            return;
        }

        if (_hit.collider.GetComponent<Node>().TryBuildTowerHere(_info, out Tower towerBuilt))
        {
            Player.Instance.Money -= _info.BuildPrice;
            Debug.Log("타워 건설 완료");         
        }
    }





}
