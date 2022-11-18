using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum StateTypes
    {
        Idle,
        Move,
        Jump,
        Fall,
        Attack,
        Slide,
        LadderUp,
        LadderDown,
        Hurt,
        Die
    }
    public StateTypes CurrentType;
    public StateBase Current;
    private Dictionary<StateTypes, StateBase> _states = new Dictionary<StateTypes, StateBase>();

    private void Awake()
    {
        InitStates();
    }

    private void Start()
    {
        Current = _states[default(StateTypes)];
        CurrentType = default(StateTypes);
    }

    /// <summary>
    /// ���¸� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="newStateType"> ��ȯ�Ϸ��� ���� Ÿ�� </param>
    public void ChangeState(StateTypes newStateType)
    {
        // �ٲٷ��� Ÿ���� ���� �������� ���¿� ���� Ÿ���̸� ���¸� ��ȯ���� �ʴ´�.
        if (CurrentType == newStateType)
            return;

        // �ٲٷ��� ���°� ���� �������� �ʴٸ� ���¸� ��ȯ���� �ʴ´�.
        if (_states[newStateType].CanExecute() == false)
            return;

        Current.Stop(); // ���� �������� ���� �ߴ�
        Current = _states[newStateType]; // �ٸ� ���·� ��ȯ
        CurrentType = newStateType; // ���� ���� Ÿ���� ��ȯ�� Ÿ������ ����
        Current.Execute(); // ��ȯ�� ���� ����
    }

    private void Update()
    {
        ChangeState(Current.Update()); // ���� ���� �� ��ȯ�ؾ��ϴ� Ÿ������ ���� ��ȯ
    }

    private void InitStates()
    {
        _states.Add(StateTypes.Idle, new StateIdle(StateTypes.Idle, this));
        _states.Add(StateTypes.Move, new StateMove(StateTypes.Move, this));
        _states.Add(StateTypes.Jump, new StateJump(StateTypes.Jump, this));
    }
}
