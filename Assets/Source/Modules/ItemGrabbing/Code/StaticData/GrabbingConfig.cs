#if UNITY_EDITOR
using Kilosoft.Tools;
#endif
using UnityEngine;

namespace ItemGrabbing
{
    [CreateAssetMenu(fileName = "Grabbing Config", menuName = "Config/Grabbing/Create grabbing config")]
    public class GrabbingConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 DropDelayClamp { get; private set; }
        [field: SerializeField] public Vector2 DropPowerClamp { get; private set; }
        [field: SerializeField] public AnimationCurve Graph { get; private set; }

        private void OnValidate()
        {
            if (Graph.keys.Length < 2)
            {
                Graph.ClearKeys();

                Graph.AddKey(new Keyframe(DropDelayClamp.x, DropPowerClamp.x));
                Graph.AddKey(new Keyframe(DropDelayClamp.y, DropPowerClamp.y));
            }
            else
            {
                Graph.keys[0] = new Keyframe(DropDelayClamp.x, DropPowerClamp.x);
                Graph.keys[Graph.keys.Length - 1] = new Keyframe(DropDelayClamp.y, DropPowerClamp.y);
            }
        }

#if UNITY_EDITOR
        [EditorButton("Recalculate Keyframes")]
        public void RecalculateKeyframes()
        {
            Graph.ClearKeys();

            Graph.AddKey(new Keyframe(DropDelayClamp.x, DropPowerClamp.x));
            Graph.AddKey(new Keyframe(DropDelayClamp.y, DropPowerClamp.y));
        }
#endif
    }
}
