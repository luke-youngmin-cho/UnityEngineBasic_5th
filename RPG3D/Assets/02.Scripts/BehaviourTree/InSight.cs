using UnityEngine;

namespace ULB.RPG.AISystems
{
    public class InSight : Behaviour, IChild
    {
        public Behaviour child { get; set; }
        private CharacterBase _owner;
        private float _radius;
        private float _angle;
        private LayerMask _targetMask;

        public InSight(CharacterBase owner, float radius, float angle, LayerMask targetMask)
        {
            _owner = owner;
            _radius = radius;
            _angle = angle;
            _targetMask = targetMask;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;
            Physics.Raycast(_owner.transform.position, Quaternion.Euler(Vector3.up * _angle / 2.0f) * Vector3.forward * _radius);
            Vector3 targetDir = new Vector3(Mathf.Sin(_angle / 2.0f), 0.0f, Mathf.Cos(_angle / 2.0f));
            Ray ray = new Ray(_owner.transform.position, targetDir);
            if (Physics.Raycast(ray, out RaycastHit hit, _radius, _targetMask))
            {
                _owner.target = hit.transform;
            }
            return Result.Success;
        }
    }
}
