using System.Collections;
using UnityEngine;

namespace Core
{
    public class AnimationCurveScaleView : IScaleView
    {
        private AnimationCurve _animationCurve;
        private Transform _transform;
        private Vector3 _amount;

        public AnimationCurveScaleView(Transform transform, AnimationCurve animationCurve, Vector3 amount)
        {
            _transform = transform;
            _animationCurve = animationCurve;
            _amount = amount;
        }

        public IEnumerator Scale(float deltaTime)
        {
            yield return ScaleRoutine(_transform, _transform.localScale + _amount, deltaTime);
        }

        public IEnumerator Unscale(float deltaTime)
        {
            yield return ScaleRoutine(_transform, _transform.localScale - _amount, deltaTime);
        }

        private IEnumerator ScaleRoutine(Transform transform, Vector3 target, float deltaTime)
        {
            float min = transform.localScale.x;
            float max = target.x;

            float elapsed = deltaTime;

            while (transform.localScale != target)
            {
                float normalized = _animationCurve.Evaluate(elapsed);
                float value = (normalized * (max - min)) + min;

                transform.localScale = new Vector3(value, value, value);

                elapsed += deltaTime;
                yield return null;
            }
        }
    }
}
