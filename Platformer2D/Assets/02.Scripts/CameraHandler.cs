using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;
    [Range(1.0f, 10.0f)]
    [SerializeField] private float _smoothness;
    private Camera _camera;

    [SerializeField] private BoxCollider2D _boundShape;
    [SerializeField] private Transform _target;

    private void OnDrawGizmosSelected()
    {
        Camera cam = Camera.main;
        Vector3 leftBottom = cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, cam.nearClipPlane));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, cam.nearClipPlane));
        Vector3 center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, rightTop - leftBottom);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boundShape.transform.position + (Vector3)_boundShape.offset, _boundShape.size);
    }
}
