using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Pathfinder))]
public class Enemy : MonoBehaviour
{
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
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(_nextPoint.position.x,
                                        _rb.position.y,
                                        _nextPoint.position.z);
        Vector3 dir = (_rb.position - targetPos).normalized;
        
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
        // todo -> 자기자신 파괴
    }
}
