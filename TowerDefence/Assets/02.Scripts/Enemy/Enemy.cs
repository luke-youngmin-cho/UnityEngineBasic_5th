using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Pathfinder))]
public class Enemy : MonoBehaviour
{
    private float _hp;
    public float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            if (_hp != value)
            {
                _hp = value;
                OnHpChanged?.Invoke(_hp);

                if (_hp <= 0)
                    OnHpMin?.Invoke();
            }
        }
    }
    public float HpMax = 100;
    public event Action OnHpMin;
    public event Action<float> OnHpChanged;

    public float Speed;

    private Pathfinder _pathfinder;
    private List<Transform> _path;
    private int _currentPathPointIndex;
    private Transform _nextPoint;
    private float _posTolerance = 0.05f;
    private Rigidbody _rb;

    public void SetPath(Transform start, Transform end)
    {
        if (_pathfinder.TryFindOptimizedPath(start, end, out _path))
        {
            _nextPoint = _path[0];
        }
        else
        {
            Debug.LogWarning("[Enemy] : 길찾기 실패했습니다.");
            gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pathfinder = GetComponent<Pathfinder>();
        Hp = HpMax;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(_nextPoint.position.x,
                                        _rb.position.y,
                                        _nextPoint.position.z);
        Vector3 dir = (targetPos - _rb.position).normalized;
        
        // 다음 포인트에 도착시
        if (Vector3.Distance(targetPos, _rb.position) < _posTolerance)
        {
            if (TryGetNextPoint(_currentPathPointIndex, out _nextPoint))
            {
                _currentPathPointIndex++;
            }
            else
            {
                OnReachedToEnd();
            }
        }

        _rb.rotation = Quaternion.LookRotation(dir);
        _rb.MovePosition(_rb.position + dir * Speed * Time.fixedDeltaTime);
    }

    private bool TryGetNextPoint(int pointIndex, out Transform nextPoint)
    {
        nextPoint = null;

        if (pointIndex < _path.Count - 1)
        {
            nextPoint = _path[pointIndex + 1];
        }

        return nextPoint;
    }

    private void OnReachedToEnd()
    {
        // todo -> 플레이어 체력 차감
        Hp = 0;
    }
}
