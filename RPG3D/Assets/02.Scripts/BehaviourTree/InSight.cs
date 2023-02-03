using System.Linq;
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
        private float _angleDelta;
        private float _height;


        public InSight(CharacterBase owner, float radius, float angle, float angleDelta, float height, LayerMask targetMask)
        {
            _owner = owner;
            _radius = radius;
            _angle = angle;
            _angleDelta = angleDelta;
            _height = height;
            _targetMask = targetMask;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            leaf = this;

            Ray ray;
            RaycastHit hit;

            for (float theta = 0; theta < _angle / 2.0f; theta += _angleDelta)
            {
                Debug.DrawRay(_owner.transform.position + Vector3.up * _height,
                              Quaternion.Euler(Vector3.up * theta) * (_owner.transform.forward * _radius),
                              Color.white);
                Debug.DrawRay(_owner.transform.position + Vector3.up * _height,
                              Quaternion.Euler(Vector3.up * -theta) * (_owner.transform.forward * _radius),
                              Color.white);
            }


            for (float theta = 0; theta < _angle / 2.0f; theta += _angleDelta)
            {
                ray = new Ray(_owner.transform.position + Vector3.up * _height,
                              Quaternion.Euler(Vector3.up * theta) * _owner.transform.forward * _radius);
                if (Physics.Raycast(ray, out hit, _radius, _targetMask))
                {
                    Debug.DrawRay(_owner.transform.position + Vector3.up * _height,
                              Quaternion.Euler(Vector3.up * theta) * (_owner.transform.forward * _radius),
                              Color.red);

                    _owner.target = hit.transform;
                    return child.Invoke(out leaf);
                }

                ray = new Ray(_owner.transform.position + Vector3.up * _height,
                              Quaternion.Euler(Vector3.up * -theta) * _owner.transform.forward * _radius);
                if (Physics.Raycast(ray, out hit, _radius, _targetMask))
                {
                    Debug.DrawRay(_owner.transform.position + Vector3.up * _height,
                              Quaternion.Euler(Vector3.up * -theta) * (_owner.transform.forward * _radius),
                              Color.red);

                    _owner.target = hit.transform;
                    return child.Invoke(out leaf);
                }
            }

            //var cols = Physics.OverlapSphere(_owner.transform.position, _radius, _targetMask)
            //    .ToList()
            //    .Where(x =>
            //    {
            //        Mathf.Acos(Mathf.Abs(x.transform.position.z - _owner.transform.position.z)
            //                    / Mathf.Sqrt(Mathf.Pow(x.transform.position.z - _owner.transform.position.z, 2) +
            //                                 Mathf.Pow(x.transform.position.x - _owner.transform.position.x, 2)))
            //        <= _angle / 2.0f;
            //    }
            //    );
            //
            //


            _owner.target = null;
            return Result.Failure;
        }
    }
}
