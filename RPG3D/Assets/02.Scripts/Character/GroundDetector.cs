using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isDetected;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _groundLayer;


    public bool CastGround(float distance, out RaycastHit hit)
    {
        return Physics.SphereCast(transform.position, _range, Vector3.down, out hit, distance, _groundLayer);
    }

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
        
        if (CastGround(0.5f, out RaycastHit hit))
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position + Vector3.forward * _range, hit.point + Vector3.forward * _range);
            Gizmos.DrawLine(transform.position + Vector3.back * _range, hit.point + Vector3.back * _range);
            Gizmos.DrawLine(transform.position + Vector3.left * _range, hit.point + Vector3.left * _range);
            Gizmos.DrawLine(transform.position + Vector3.right * _range, hit.point + Vector3.right * _range);

            Gizmos.DrawWireSphere(transform.position, _range);
            Gizmos.DrawWireSphere(hit.point, _range);
        }     
    }
}
