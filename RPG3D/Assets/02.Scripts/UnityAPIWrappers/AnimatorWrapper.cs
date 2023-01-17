using UnityEngine;

namespace ULB.RPG
{
    /// <summary>
    /// Animator 정보/상태를 래핑하는 클래스
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorWrapper : MonoBehaviour
    {
        public bool isPreviousStateFinished { get => true; }
        public bool isPreviousMachineFinished { get => true; }
        

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






    }
}

