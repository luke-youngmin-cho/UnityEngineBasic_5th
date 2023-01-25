using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ULB.RPG.InputSystems
{
    /// <summary>
    /// Ư�� Ű�Է��϶� � �׼��� ���Ұ��������� Dictionary �� ����س��� 
    /// ��� �׼��� �� ������ ��ȸ�ϸ鼭 ȣ������.
    /// </summary>
    public class KeyInputHandler : MonoBehaviour
    {
        public static KeyInputHandler instance;

        private Dictionary<KeyCode, Action> _keyDownActions = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> _keyPressActions = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> _keyUpActions = new Dictionary<KeyCode, Action>();

        public bool mouse0Trigger
        {
            get
            {
                if (_mouse0Trigger)
                {
                    _mouse0Trigger = false;
                    return true;
                }
                return false;
            }
            set
            {
                _mouse0Trigger = value;
                if (value)
                    OnMouse0TriggerActivated?.Invoke();
            }
        }
        private bool _mouse0Trigger;
        public event Action OnMouse0TriggerActivated;


        public void RegisterKeyDownAction(KeyCode key, Action action)
        {
            if (_keyDownActions.ContainsKey(key))
                _keyDownActions[key] += action;
            else
                _keyDownActions.Add(key, action);
        }
        public void RegisterKeyPressAction(KeyCode key, Action action)
        {
            if (_keyPressActions.ContainsKey(key))
                _keyPressActions[key] += action;
            else
                _keyPressActions.Add(key, action);
        }
        public void RegisterKeyUpAction(KeyCode key, Action action)
        {
            if (_keyUpActions.ContainsKey(key))
                _keyUpActions[key] += action;
            else
                _keyUpActions.Add(key, action);
        }


        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouse0Trigger = true;
            }

            foreach (var pair in _keyDownActions)
            {
                if (Input.GetKeyDown(pair.Key))
                    pair.Value.Invoke();
            }

            foreach (var pair in _keyPressActions)
            {
                if (Input.GetKey(pair.Key))
                    pair.Value.Invoke();
            }

            foreach (var pair in _keyUpActions)
            {
                if (Input.GetKey(pair.Key))
                    pair.Value.Invoke();
            }
        }
    }
}