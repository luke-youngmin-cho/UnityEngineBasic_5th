using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Node : MonoBehaviour
{
    public bool IsTowerExist => TowerBuilt;
    public Tower TowerBuilt;
    private Renderer _renderer;
    private Material _origin;
    [SerializeField] private Material _buildAvailable;
    [SerializeField] private Material _buildNotAvailable;

    public bool TryBuildTowerHere(TowerInfo info, out Tower built)
    {
        built = null;

        if (IsTowerExist)
        {
            Debug.Log("해당 위치에는 타워가 이미 존재하므로 건설할 수 없습니다.");
            return false;
        }

        if (TowerAssets.Instance.TryGetTower(info, out Tower prefab))
        {
            built = Instantiate(prefab,
                                transform.position,
                                Quaternion.identity);
            built.Node = this;
            TowerBuilt = built;
            return true;
        }

        return false;
    }

    public Tower BuildTowerHere(Tower prefab)
    {
        if (IsTowerExist)
            Destroy(TowerBuilt.gameObject);

        TowerBuilt = Instantiate(prefab,
                                 transform.position,
                                 Quaternion.identity);
        TowerBuilt.Node = this;
        return TowerBuilt;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _origin = _renderer.sharedMaterial;
    }

    private void OnMouseEnter()
    {
        if (IsTowerExist)
            _renderer.material = _buildNotAvailable;
        else
            _renderer.material = _buildAvailable;
    }

    private void OnMouseExit()
    {
        _renderer.material = _origin;
    }
}
