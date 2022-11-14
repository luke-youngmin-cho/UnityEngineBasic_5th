using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum States
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Die
    }
    public States Current;

    // animation
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();        
    }

    private void Update()
    {
        ChangeState(UpdateState());
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
                    Idle().Reset();
                    break;
                case States.Move:
                    Move().Reset();
                    break;
                case States.Attack:
                    Attack().Reset();
                    break;
                case States.Hurt:
                    Hurt().Reset(); 
                    break;
                case States.Die:
                    Die().Reset();
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
                return Idle().Current;
            case States.Move:
                return Move().Current;
            case States.Attack:
                return Attack().Current;
            case States.Hurt:
                return Hurt().Current;
            case States.Die:
                return Die().Current;
            default:
                throw new System.Exception("Wrong state");
        }
    }

    // Idle ���� ���� ����
    private bool CanIdle()
    {
        return true;
    }

    // Idle ���� ����
    private IEnumerator<States> Idle()
    {
        _animator.Play("Idle");
        yield return States.Idle;
    }

    // Move ���� ���� ����
    private bool CanMove()
    {
        // todo -> Check ground detected
        return true;
    }

    // Move ���� ����
    private IEnumerator<States> Move()
    {
        _animator.Play("Move");
        yield return States.Move;
    }

    // Attack ���� ���� ����
    private bool CanAttack()
    {
        // todo -> Check target in attack range && !Hurt && !Die
        return true;
    }

    // Attack ���� ����
    private IEnumerator<States> Attack()
    {
        _animator.Play("Attack");
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return States.Attack;
        }
        yield return States.Idle;
    }

    // Hurt ���� ���� ����
    private bool CanHurt()
    {
        return true;
    }

    // Hurt ���� ����
    private IEnumerator<States> Hurt()
    {
        _animator.Play("Hurt");
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return States.Hurt;
        }
        yield return States.Idle;
    }

    // Die ���� ���� ����
    private bool CanDie()
    {
        return true;
    }

    // Die ���� ����
    private IEnumerator<States> Die()
    {
        _animator.Play("Die");
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return States.Attack;
        }

        Destroy(gameObject);
        yield return States.Idle;
    }
}
