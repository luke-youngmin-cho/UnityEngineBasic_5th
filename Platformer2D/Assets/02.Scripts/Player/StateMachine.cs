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
    [SerializeField] private StateTypes _currentType;
    private StateBase _current;
    private Dictionary<StateTypes, StateBase> _states;

}
