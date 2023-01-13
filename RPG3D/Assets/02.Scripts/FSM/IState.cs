using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    /// <summary>
    /// FSM �� ���� ���̽� �������̽�
    /// ���¸� ������ �� �ִ� ����, Ư�� �����϶� Ư�� id �� ���·� ��ȯ�ϴ� transitions �� ������ 
    /// �ش� ���¿����� ������ Update �� ȣ�����ִ� ���·� ����Ѵ�.
    /// </summary>
    public interface IState
    {
        int id { get; set; }
        Func<bool> canExecute { get; set; } // �� ���¸� ������ �� �ִ� ����
        List<KeyValuePair<Func<bool>, int>> transitions { get; set; } // Ư�� ���¿��� Ư�� id �� ���·� ��ȯ�ϴ� ���
        void Execute();
        void Stop();

        /// <summary>
        /// ���� ����
        /// </summary>
        /// <returns> ��ȯ�ؾ��ϴ� ���� ������ id </returns>
        int Update();
    }
}