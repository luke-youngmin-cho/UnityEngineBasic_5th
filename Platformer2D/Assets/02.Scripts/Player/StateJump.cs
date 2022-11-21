using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJump : StateBase
{
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    public StateJump(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _groundDetector = machine.GetComponent<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
    }

    public override bool CanExecute()
    {
        return (Machine.CurrentType == StateMachine.StateTypes.Idle ||
                Machine.CurrentType == StateMachine.StateTypes.Move) &&
                _groundDetector.IsDetected;
    }

    public override void Execute()
    {
        base.Execute();
        Animator.Play("Jump");
        Movement.DirectionChangable = true;
        Movement.Movable = false;        
    }

    public override StateMachine.StateTypes Update()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            // 순간 Y 속도 0 만들고 위 방향으로 힘 가하기
            //--------------------------------------
            case Commands.Prepare:
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                    _rb.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                    MoveNext();
                }
                break;
            // 성공적으로 점프 되었는지 (발이 그라운드에서 떨어졌는지)
            //------------------------------------------------
            case Commands.Casting:
                {
                    if (_groundDetector.IsDetected == false)
                        MoveNext();
                }
                break;
            // Y 속도가 음수가 되는순간 점프 액션 종료
            //------------------------------------------------
            case Commands.OnAction:
                {
                    if (_rb.velocity.y < 0)
                        MoveNext();
                }
                break;
            // 점프 상승 끝났으니 떨어지는 Fall 상태로 전환
            //--------------------------------------------------
            case Commands.Finish:
                {
                    next = StateMachine.StateTypes.Fall;
                }
                break;
            default:
                break;
        }

        return next;
    }
}
