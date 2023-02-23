using System.Collections;
using UnityEngine;

namespace ULB.RPG.Controllers
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetMask;

        private void OnTriggerStay(Collider other)
        {
            if (((1 << other.gameObject.layer) & _targetMask) > 0)
            {
                if (other.TryGetComponent(out ItemController controller))
                {
                    controller.Pick(transform);
                }
            }
        }
    }
}