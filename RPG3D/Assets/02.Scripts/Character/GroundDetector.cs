using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isDetected;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _groundLayer;

    private void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + _offset,
                                                _range,
                                                _groundLayer);

        isDetected = cols.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _offset, _range);
    }
}
