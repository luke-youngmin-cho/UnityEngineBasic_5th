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
        // �̵����� = �ӵ� * �ð�
        // �̵����ͺ�ȭ�� = �ӵ� * �ð���ȭ��
        //transform.position += dir * Time.fixedDeltaTime;
        transform.Translate(dir * MoveSpeed * Time.fixedDeltaTime);

        // Quaternion
        // �����, ������ 4���� ���ҷ� ǥ���ϱ����� ü��
        // Eular ������ ����ϸ� ������ ������ ����.
        // ������ : �ΰ��� ���� ��ġ�鼭 �������� ����ϴ� ����
        // ������ Ealar ����ü�躸�� ������
        transform.Rotate(Vector3.up * _r * RotateSpeed * Time.fixedDeltaTime);
    }
}
