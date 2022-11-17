using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool IsDetected;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] private LayerMask _groundLayer;

    private void FixedUpdate()
    {
        IsDetected = Physics2D.OverlapBox((Vector2)transform.position + _offset,
                                          _size,
                                          0.0f,
                                          _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_offset,
                            _size);
    }
}
