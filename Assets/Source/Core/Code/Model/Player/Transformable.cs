using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class Transformable
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Quaternion _rotation;

        public Transformable(Transform transform)
        {
            _position = transform.position;
            _scale = transform.localScale;
            _rotation = transform.rotation;
        }

        public Vector3 Position => _position;
        public Vector3 Scale => _scale;
        public Quaternion Rotation => _rotation;

        public void SetPosition(Vector3 position)
        {
            if (position == null)
                throw new ArgumentNullException();

            _position = position;
        }

        public void IncreaseScale(Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException();

            _scale = _scale + scale;
        }
        
        public void DecreaseScale(Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException();

            _scale = _scale - scale;
        }
    }
}
