using UnityEngine;

namespace ULB.RPG
{
    /// <summary>
    /// Animator 정보/상태를 래핑하는 클래스
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorWrapper : MonoBehaviour
    {
        public bool isPreviousStateFinished { get => _monitorOnStateHashMem == _monitorOffStateHash ||
                                                     _monitorOnStateHashMem == 0; }
        public bool isPreviousMachineFinished { get => _monitorOnMachineHashMem == _monitorOffMachineHash ||
                                                       _monitorOnMachineHashMem == 0; }

        [SerializeField] private int _monitorOnMachineHash; // 감시자가 막 켜진 애니메이터 서브머신의 해시코드
        [SerializeField] private int _monitorOnMachineHashMem; // 감시자가 직전에 켜졌었던 애니메이터 서브머신의 해시코드
        [SerializeField] private int _monitorOffMachineHash; // 감시자가 막 꺼진 애니메이터 서브머신의 해시코드

        [SerializeField] private int _monitorOnStateHash; // 감시자가 막 켜진 애니메이터 상태의 해시코드
        [SerializeField] private int _monitorOnStateHashMem; // 감시자가 직전에 켜졌었던 애니메이터 상태의 해시코드
        [SerializeField] private int _monitorOffStateHash; // 감시자가 막 꺼진 애니메이터 상태의 해시코드

        [SerializeField] private Animator _animator;

        public float GetCurrentNormalizedTime(int layer = 0) 
            => _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;

        public bool GetBool(string paramName) => _animator.GetBool(paramName);
        public void SetBool(string paramName, bool value) => _animator.SetBool(paramName, value);
        public float GetFloat(string paramName) => _animator.GetFloat(paramName);
        public void SetFloat(string paramName, float value) => _animator.SetFloat(paramName, value);
        public int GetInt(string paramName) => _animator.GetInteger(paramName);
        public void SetInt(string paramName, int value) => _animator.SetInteger(paramName, value);
        public void SetTrigger(string paramName) => _animator.SetTrigger(paramName);


        private void Awake()
        {
            InitMachineMonitors();
        }

        private void InitMachineMonitors()
        {
            foreach (AnimatorMachineMonitor monitor in _animator.GetBehaviours<AnimatorMachineMonitor>())
            {
                monitor.OnEnter += (hash) =>
                {
                    _monitorOnMachineHashMem = _monitorOnMachineHash;
                    _monitorOnMachineHash = hash;
                };

                monitor.OnExit += (hash) =>
                {
                    _monitorOffMachineHash = hash;
                };
            }

            foreach (AnimatorStateMonitor monitor in _animator.GetBehaviours<AnimatorStateMonitor>())
            {
                monitor.OnEnter += (hash) =>
                {
                    _monitorOnStateHashMem = _monitorOnStateHash;
                    _monitorOnStateHash = hash;
                };

                monitor.OnExit += (hash) =>
                {
                    _monitorOffStateHash = hash;
                };
            }

        }

    }
}

