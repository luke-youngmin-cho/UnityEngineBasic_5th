using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Dash,
        Crouch,
        Slide,
        LadderUp,
        LadderDown,
        Hurt,
        Die
    }
    public StateTypes CurrentType;
    public StateTypes PreviousType;
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

        RegisterShortcuts();
    }

    /// <summary>
    /// 상태를 전환하는 함수
    /// </summary>
    /// <param name="newStateType"> 전환하려는 상태 타입 </param>
    public void ChangeState(StateTypes newStateType)
    {
        // 바꾸려는 타입이 현재 수행중인 상태와 같은 타입이면 상태를 전환하지 않는다.
        if (CurrentType == newStateType)
            return;

        // 바꾸려는 상태가 실행 가능하지 않다면 상태를 전환하지 않는다.
        if (_states[newStateType].CanExecute() == false)
            return;

        Current.Stop(); // 현재 수행중인 상태 중단
        Current = _states[newStateType]; // 다른 상태로 전환
        PreviousType = CurrentType; // 이전 상태 기억
        CurrentType = newStateType; // 현재 상태 타입을 전환한 타입으로 갱신
        Current.Execute(); // 전환된 상태 실행
    }

    private void Update()
    {
        ChangeState(Current.Update()); // 상태 수행 후 전환해야하는 타입으로 상태 전환
    }

    private void InitStates()
    {
        _states.Add(StateTypes.Idle, new StateIdle(StateTypes.Idle, this));
        _states.Add(StateTypes.Move, new StateMove(StateTypes.Move, this));
        _states.Add(StateTypes.Jump, new StateJump(StateTypes.Jump, this));
        _states.Add(StateTypes.Fall, new StateFall(StateTypes.Fall, this));
        _states.Add(StateTypes.Dash, new StateDash(StateTypes.Dash, this));
        _states.Add(StateTypes.Slide, new StateSlide(StateTypes.Slide, this));
        _states.Add(StateTypes.Crouch, new StateCrouch(StateTypes.Crouch, this));
    }


    
    //=====================================================================================
    //                             단축키 등록
    //=====================================================================================
    
    private void RegisterShortcuts()
    {
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.LeftAlt, () => ChangeState(StateTypes.Jump));
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.LeftShift, () => ChangeState(StateTypes.Dash));
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.X, () => ChangeState(StateTypes.Slide));
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.DownArrow, () => ChangeState(StateTypes.Crouch));
    }
}
