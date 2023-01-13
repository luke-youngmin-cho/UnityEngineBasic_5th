using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    /// <summary>
    /// FSM 의 상태 베이스 인터페이스
    /// 상태를 실행할 수 있는 조건, 특정 상태일때 특정 id 의 상태로 전환하는 transitions 를 가지며 
    /// 해당 상태에서의 로직은 Update 를 호출해주는 형태로 사용한다.
    /// </summary>
    public interface IState
    {
        int id { get; set; }
        Func<bool> canExecute { get; set; } // 이 상태를 실행할 수 있는 조건
        List<KeyValuePair<Func<bool>, int>> transitions { get; set; } // 특정 상태에서 특정 id 의 상태로 전환하는 목록
        void Execute();
        void Stop();

        /// <summary>
        /// 상태 로직
        /// </summary>
        /// <returns> 전환해야하는 다음 상태의 id </returns>
        int Update();
    }
}