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
            // ���� Y �ӵ� 0 ����� �� �������� �� ���ϱ�
            //--------------------------------------
            case Commands.Prepare:
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                    _rb.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                    MoveNext();
                }
                break;
            // ���������� ���� �Ǿ����� (���� �׶��忡�� ����������)
            //------------------------------------------------
            case Commands.Casting:
                {
                    if (_groundDetector.IsDetected == false)
                        MoveNext();
                }
                break;
            // Y �ӵ��� ������ �Ǵ¼��� ���� �׼� ����
            //------------------------------------------------
            case Commands.OnAction:
                {
                    if (_rb.velocity.y < 0)
                        MoveNext();
                }
                break;
            // ���� ��� �������� �������� Fall ���·� ��ȯ
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
