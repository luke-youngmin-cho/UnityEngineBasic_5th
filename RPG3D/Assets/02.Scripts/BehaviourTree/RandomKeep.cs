using System.Collections;
using UnityEngine;

namespace ULB.RPG.AISystems
{
    public class RandomKeep : Behaviour, IChild
    {
        private float _minTime;
        private float _maxTime;
        private float _timeMark;
        private float _keepTime;
        public Behaviour child { get; set; }

        public RandomKeep(float minTime, float maxTime)
        {
            _minTime = minTime;
            _maxTime = maxTime;
        }


        public override Result Invoke(out Behaviour leaf)
        {
            Result result;
            if (Time.time - _timeMark > _keepTime)
            {
                result = child.Invoke(out leaf);
                if (result == Result.Success)
                {
                    _timeMark = Time.time;
                    _keepTime = Random.Range(_minTime, _maxTime);
                }
            }
            else
            {
                result = child.Invoke(out leaf);
                if (result != Result.Success)
                {
                    _timeMark = 0.0f;
                    _keepTime = 0.0f;
                    return result;
                }

                leaf = this;
                result = Result.Running;
            }

            return result;
        }
    }
}