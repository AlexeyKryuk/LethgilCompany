using Core.View;
using UnityEngine;

namespace Core
{
    public class CharacterControllerViewTest : MonoBehaviour, ICharacterControllerView
    {
        public Transform Transform => transform;
        public Transform CameraFollowPoint => transform;

        public void UpdateInputs(ICharacterInputs inputs, Quaternion cameraRotation)
        {
            Debug.Log("Set Inputs " + inputs);
        }
    }
}
