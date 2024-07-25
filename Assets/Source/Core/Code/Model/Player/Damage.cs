using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    [Serializable]
    public class Damage
    {
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        public Damage(float minValue, float maxValue)
        {
            if (maxValue < minValue)
                throw new ArgumentOutOfRangeException();

            _minValue = minValue;
            _maxValue = maxValue;
        }

        public float MinValue => _minValue;
        public float MaxValue => _maxValue;

        public float Value => Random.Range(_minValue, _maxValue);

        public void IncreaseGeneralValue(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException();

            _minValue += amount;
            _maxValue += amount;
        }

        public void DecreaseGeneralValue(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException();

            _minValue = MathF.Max(0, _minValue - amount);
            _maxValue = MathF.Max(0, _maxValue - amount);
        }
    }
}
