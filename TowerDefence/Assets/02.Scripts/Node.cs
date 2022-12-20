using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool IsTowerExist;

    private Renderer _renderer;
    private Material _origin;
    [SerializeField] private Material _buildAvailable;
    [SerializeField] private Material _buildNotAvailable;

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
