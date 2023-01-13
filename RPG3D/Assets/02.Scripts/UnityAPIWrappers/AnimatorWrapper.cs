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
    }
}

