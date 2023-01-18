using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ULB.RPG.InputSystems
{
    /// <summary>
    /// 특정 키입력일때 어떤 액션을 취할것인지들을 Dictionary 로 등록해놓고 
    /// 모든 액션을 매 프레임 순회하면서 호출해줌.
    /// </summary>
    public class KeyInputHandler : MonoBehaviour
    {
        public static KeyInputHandler instance;

        private Dictionary<KeyCode, Action> _keyDownActions = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> _keyPressActions = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> _keyUpActions = new Dictionary<KeyCode, Action>();

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