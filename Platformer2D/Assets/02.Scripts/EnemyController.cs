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

    private void UpdateState()
    {
        switch (Current)
        {
            case States.Idle:
                break;
            case States.Move:
                break;
            case States.Attack:
                break;
            case States.Hurt:
                break;
            case States.Die:
                break;
            default:
                break;
        }
    }
}
