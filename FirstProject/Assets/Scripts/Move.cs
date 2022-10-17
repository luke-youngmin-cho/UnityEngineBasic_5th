using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float _h;
    float _v;

    private void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(_h, 0.0f, _v).normalized;

        transform.position += dir * Time.fixedDeltaTime;
    }
}
