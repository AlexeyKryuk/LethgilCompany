using Core.View;
using UnityEngine;
using UnityEngine.Windows;

namespace Core
{
    public class CharacterControllerViewTest : MonoBehaviour, ICharacterControllerView
    {
        public Transform Transform => transform;
        public Transform CameraTarget => transform;
        public Transform CameraFollow => transform;

        public void SpecifyCameraTransform(Transform transform)
        {
            Debug.Log("transform camera " + transform);
        }

        public void UpdateInputs(ICharacterInputs inputs)
        {
            Debug.Log("Set Inputs " + inputs);
        }
    }
}
