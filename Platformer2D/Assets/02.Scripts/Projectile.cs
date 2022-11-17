using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Dir;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _mapBoundLayer;

    private void FixedUpdate()
    {
        transform.position += Dir * _speed * Time.fixedDeltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (1<<other.gameObject.layer == _mapBoundLayer)
            Destroy(gameObject);
    }
}
