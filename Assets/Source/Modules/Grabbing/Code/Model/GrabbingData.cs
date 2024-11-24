using UnityEngine;

namespace ItemGrabbing
{
    public class GrabbingData
    {
        private readonly AnimationCurve _graph;

        public GrabbingData(AnimationCurve graph)
        {
            _graph = graph;
        }

        public float GetDropPower(float holdTime)
            => _graph.Evaluate(holdTime);
    }
}
