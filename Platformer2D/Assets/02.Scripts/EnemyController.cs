using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // States
    public enum States
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Die
    }
    public States Current;
    private int _idleStep;
    private int _moveStep;
    private int _attackStep;
    private int _hurtStep;
    private int _dieStep;

    // AI
    public enum AI
    {
        Idle,
        Think,
        TakeARest,
        MoveLeft,
        MoveRight,
        FollowTarget,
        AttackTarget
    }
    [SerializeField] private AI _ai;
    [SerializeField] private bool _aiAutoFollow = false;
    [SerializeField] private float _aiDetectRange = 2.0f;
    [SerializeField] private bool _aiAttackEnable = false;
    [SerializeField] private float _aiAttackRange = 0.5f;
    [SerializeField] private float _aiBehaviorTimeMin = 0.1f;
    [SerializeField] private float _aiBehaviorTimeMax = 2.0f;
    private float _aiBehaviorTimer;
    [SerializeField] private LayerMask _targetLayer;

    // Movement
    private const int DIRECTION_LEFT = -1;
    private const int DIRECTION_RIGHT = 1;
    private int _direction;
    public int Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value > 0)
            {
                _direction = DIRECTION_RIGHT;
                transform.eulerAngles = Vector3.zero;
            }
            else if (value < 0)
            {
                _direction = DIRECTION_LEFT;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            } 
        }
    }
    [SerializeField] private bool _moveEnable = true;
    [SerializeField] private bool _movable = false;
    [SerializeField] private float _moveSpeed = 1.0f;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;

    // animation
    private Animator _animator;

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();        
    }

    private void Update()
    {
        UpdateAI();
        ChangeState(UpdateState());
    }

    private void FixedUpdate()
    {
        if (_moveEnable && _movable)
            _rb.MovePosition(_rb.position + Direction * Vector2.right * _moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateAI()
    {
        if (Current == States.Hurt || Current == States.Die)
            return;

        Collider2D target = null;
        switch (_ai)
        {
            case AI.Idle:
                break;
            case AI.Think:
                {
                    target = Physics2D.OverlapCircle(_rb.position, _aiDetectRange, _targetLayer);
                    if (target != null)
                    {
                        if (_aiAttackEnable &&
                            Vector2.Distance(_rb.position, target.transform.position) <= _aiAttackRange)
                        {
                            _ai = AI.AttackTarget;
                            ChangeState(States.Attack);
                        }
                        else
                        {
                            _ai = AI.FollowTarget;
                            ChangeState(States.Move);
                        }
                    }
                    else
                    {
                        _ai = (AI)Random.Range((int)AI.TakeARest, (int)AI.MoveRight + 1);
                        _aiBehaviorTimer = Random.Range(_aiBehaviorTimeMin, _aiBehaviorTimeMax);
                        if (_ai == AI.TakeARest)
                            ChangeState(States.Idle);
                        else
                            ChangeState(States.Move);
                    }
                }
                break;
            case AI.TakeARest:
                {
                    if (_aiBehaviorTimer <= 0)
                    {
                        _ai = AI.Think;
                    }
                    else
                    {
                        _aiBehaviorTimer -= Time.deltaTime;
                    }
                }
                break;
            case AI.MoveLeft:
                {
                    if (_aiBehaviorTimer <= 0)
                    {
                        _ai = AI.Think;
                    }
                    else
                    {
                        Direction = DIRECTION_LEFT;
                        _aiBehaviorTimer -= Time.deltaTime;
                    }
                }
                break;
            case AI.MoveRight:
                {
                    if (_aiBehaviorTimer <= 0)
                    {
                        _ai = AI.Think;
                    }
                    else
                    {
                        Direction = DIRECTION_RIGHT;
                        _aiBehaviorTimer -= Time.deltaTime;
                    }
                }
                break;
            case AI.FollowTarget:
                {
                    if (_rb.position.x < target.transform.position.x + _col.size.x)
                    {
                        Direction = DIRECTION_RIGHT;
                    }
                    else if (_rb.position.x > target.transform.position.x - _col.size.x)
                    {
                        Direction = DIRECTION_LEFT;
                    }
                }
                break;
            case AI.AttackTarget:
                {
                    if (Current != States.Attack)
                    {
                        _ai = AI.Think;
                    }
                }
                break;
            default:
                break;
        }
    }

    public bool ChangeState(States newState)
    {
        if (Current == newState)
            return false;

        bool result = false;        

        switch (newState)
        {
            case States.Idle:
                result = CanIdle();
                break;
            case States.Move:
                result = CanMove();
                break;
            case States.Attack:
                result = CanAttack();
                break;
            case States.Hurt:
                result = CanHurt();
                break;
            case States.Die:
                result = CanDie();
                break;
            default:
                break;
        }

        if (result)
        {
            switch (newState)
            {
                case States.Idle:
                    _idleStep = 1;
                    _movable = false;
                    break;
                case States.Move:
                    _moveStep = 1;
                    _movable = true;
                    break;
                case States.Attack:
                    _attackStep = 1;
                    _movable = false;
                    break;
                case States.Hurt:
                    _hurtStep = 1;
                    _movable = false;

                    break;
                case States.Die:
                    _dieStep = 1;
                    _movable = false;
                    break;
                default:
                    break;
            }
            Current = newState;
        }

        return result;
    }

    private States UpdateState()
    {
        switch (Current)
        {
            case States.Idle:
                return Idle();
            case States.Move:
                return Move();
            case States.Attack:
                return Attack();
            case States.Hurt:
                return Hurt();
            case States.Die:
                return Die();
            default:
                throw new System.Exception("Wrong state");
        }
    }

    // Idle 실행 가능 조건
    private bool CanIdle()
    {
        return true;
    }

    // Idle 상태 로직

    private States Idle()
    {
        switch (_idleStep)
        {
            case 0:
                // nothing to do
                break;
            case 1:
                {
                    _animator.Play("Idle");
                    _idleStep++;
                }
                break;
            default:
                break;
        }

        return States.Idle;
    }

    // Move 실행 가능 조건
    private bool CanMove()
    {
        // todo -> Check ground detected
        return true;
    }

    // Move 상태 로직
    private States Move()
    {
        switch (_moveStep)
        {
            case 0:
                // nothing to do
                break;
            case 1:
                {
                    _animator.Play("Move");
                    _moveStep++;
                }
                break;
            default:
                break;
        }

        return States.Move;
    }

    // Attack 실행 가능 조건
    private bool CanAttack()
    {
        // todo -> Check target in attack range && !Hurt && !Die
        return true;
    }

    // Attack 상태 로직
    private States Attack()
    {
        States next = States.Attack;

        switch (_attackStep)
        {
            case 0:
                // nothing to do
                break;
            case 1:
                {
                    _animator.Play("Attack");
                    _attackStep++;
                }
                break;
            case 2:
                {
                    if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        _attackStep++;
                    }
                }
                break;
            default:
                {
                    next = States.Idle;
                }
                break;
        }

        return next;
    }

    // Hurt 실행 가능 조건
    private bool CanHurt()
    {
        return true;
    }

    // Hurt 상태 로직
    private States Hurt()
    {
        States next = States.Hurt;

        switch (_hurtStep)
        {
            case 0:
                // nothing to do
                break;
            case 1:
                {
                    _animator.Play("Hurt");
                    _hurtStep++;
                }
                break;
            case 2:
                {
                    if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        _hurtStep++;
                    }
                }
                break;
            default:
                {
                    next = States.Idle;
                }
                break;
        }

        return next;
    }

    // Die 실행 가능 조건
    private bool CanDie()
    {
        return true;
    }

    // Die 상태 로직
    private States Die()
    {
        States next = States.Die;

        switch (_dieStep)
        {
            case 0:
                // nothing to do
                break;
            case 1:
                {
                    _animator.Play("Die");
                    _dieStep++;
                }
                break;
            case 2:
                {
                    if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        Destroy(gameObject);
                        _dieStep++;
                    }
                }
                break;
            default:
                {                    
                    next = States.Idle;
                }
                break;
        }

        return next;
    }
}
