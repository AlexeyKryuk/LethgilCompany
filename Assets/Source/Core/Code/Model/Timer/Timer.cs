using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class Timer : ITimer
    {
        [SerializeField] private float _target;
        [SerializeField] private float _current;
        [SerializeField] private bool _isStarted;

        public bool IsOver => _current >= _target;
        public bool IsStarted => _isStarted;

        public Timer(float target)
        {
            _target = target;
        }

        public void Start() => _isStarted = true;

        public void Tick(float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException();

            _current += deltaTime;

            if (IsOver)
                _isStarted = false;
        }
    }
}
