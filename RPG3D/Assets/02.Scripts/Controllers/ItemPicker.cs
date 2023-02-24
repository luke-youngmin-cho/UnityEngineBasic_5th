using System.Collections;
using UnityEngine;

namespace ULB.RPG.Controllers
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetMask;
        private bool _doPick;

        private void Update()
        {
            _doPick = Input.GetKey(KeyCode.Z);
        }

        private void OnTriggerStay(Collider other)
        {
            if (((1 << other.gameObject.layer) & _targetMask) > 0 &&
                _doPick)
            {
                if (other.TryGetComponent(out ItemController controller))
                {
                    controller.Pick(transform);
                }
            }
        }
    }
}