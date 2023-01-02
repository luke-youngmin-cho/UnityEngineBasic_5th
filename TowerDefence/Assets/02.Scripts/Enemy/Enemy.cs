using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Pathfinder))]
public class Enemy : MonoBehaviour, IDamageable, ISpeed
{
    [SerializeField] private float _hp;
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
    public event Action OnReachedToEnd;
    public int Price;
    [SerializeField]private float _speed;
    public float Speed {
        get => _speed;
        set => _speed = value;
    }
    public float SpeedOrigin = 1.0f;

    private Pathfinder _pathfinder;
    private List<Transform> _path;
    private int _currentPathPointIndex;
    private Transform _nextPoint;
    private float _posTolerance = 0.05f;
    private Rigidbody _rb;

    public BuffManager<Enemy> BuffManager { get; private set; }

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

    public void Damage(float amount)
    {
        Hp -= amount;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pathfinder = GetComponent<Pathfinder>();        
        BuffManager = new BuffManager<Enemy>(this);
    }

    private void Start()
    {
        OnHpMin += () => Player.Instance.Money += Price;
    }

    private void OnEnable()
    {
        Hp = HpMax;
        Speed = SpeedOrigin;
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
                ReachedToEnd();
            }
        }

        _rb.rotation = Quaternion.LookRotation(dir);

        if (_speed != SpeedOrigin)
        {
            Debug.Log($"스피드바뀜 {Speed}");
        }

        _rb.MovePosition(_rb.position + dir * _speed * Time.fixedDeltaTime);
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

    private void ReachedToEnd()
    {
        Player.Instance.Life -= 1;
        OnReachedToEnd?.Invoke();
        ObjectPool.Instance.Return(gameObject);
    }
}
