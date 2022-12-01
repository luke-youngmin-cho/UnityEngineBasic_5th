using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Owner;
    public Vector3 Dir;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _mapBoundLayer;

    private void FixedUpdate()
    {
        transform.position += Dir * _speed * Time.fixedDeltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == _mapBoundLayer)
            Destroy(gameObject);
    }
}
