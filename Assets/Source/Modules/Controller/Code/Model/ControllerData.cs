using System;
using UnityEngine;

namespace MovementController
{
    [Serializable]
    public class ControllerData
    {
        public ControllerSettings ControllerSettings;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public ControllerData(Vector3 position, Quaternion rotation, Vector3 scale, ControllerSettings controllerSettings)
        {
            ControllerSettings = controllerSettings;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}
