using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public float RotateSpeed = 360.0f;
    float _h;
    float _v;
    float _r;

    private void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        _r = Input.GetAxis("Mouse X");
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(_h, 0.0f, _v).normalized;
        // 이동벡터 = 속도 * 시간
        // 이동벡터변화량 = 속도 * 시간변화량
        //transform.position += dir * Time.fixedDeltaTime;
        transform.Translate(dir * MoveSpeed * Time.fixedDeltaTime, Space.World);

        // Quaternion
        // 사원수, 각도를 4개의 원소로 표현하기위한 체계
        // Eular 각도를 사용하면 짐벌락 문제가 생김.
        // 짐벌락 : 두개의 축이 겹치면서 자유도를 상실하는 현상
        // 연산이 Ealar 각도체계보다 가볍다
        transform.Rotate(Vector3.up * _r * RotateSpeed * Time.fixedDeltaTime);
    }
}
