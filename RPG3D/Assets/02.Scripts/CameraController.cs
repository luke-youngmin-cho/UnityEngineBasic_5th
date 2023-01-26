using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistance = 3.0f; // Ÿ�ٰ��� �ּҰŸ�
    [SerializeField] private float _maxDistance = 30.0f; // Ÿ�ٰ��� �ִ�Ÿ�
    [SerializeField] private float _wheelSpeed = 500.0f; // ���콺 ��ũ�� �� �ӵ�
    [SerializeField] private float _xMoveSpeed = 500.0f; // ���콺 x�� �̵� �ӵ�
    [SerializeField] private float _yMoveSpeed = 250.0f; // ���콺 y�� �̵� �ӵ�
    [SerializeField] private float _yLimitMin = 5.0f; // ���콺 y �� ����
    [SerializeField] private float _yLimitMax = 80.0f; // ���콺 y �� ����
    private float _y, _x, _distance; // ���콺 y��, x�� ��, ���� �Ÿ�

    private void Awake()
    {
        _distance = Vector3.Distance(transform.position, _target.position);
        _y = transform.eulerAngles.x;
        _x = transform.eulerAngles.y;
    }

    private void Start()
    {
        CursorController.instance.DeactiveCursor();
    }

    private void Update()
    {
        _x += Input.GetAxis("Mouse X") * _xMoveSpeed * Time.deltaTime;
        _y += Input.GetAxis("Mouse Y") * _yMoveSpeed * Time.deltaTime;
        _x = ClampAngle(_x, -360.0f, 360.0f);
        _y = ClampAngle(_y, _yLimitMin, _yLimitMax);

        _distance -= Input.GetAxis("Mouse ScrollWheel") * _wheelSpeed * Time.deltaTime;
        _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
    }

    private void FixedUpdate()
    {
        _target.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(_y, _x, 0.0f);
        transform.position = _target.position - transform.rotation * Vector3.forward * _distance;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        angle %= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }
}
