using System.Collections.Generic;
using UnityEngine;

namespace RockPaperScissors
{
    public class CircleLayout : MonoBehaviour
    {
        [Tooltip("Радиус круга.")]
        [SerializeField] private float _radius = 100f;

        [Tooltip("Угол смещения. Позволяет начать расстановку не с нуля градусов.")]
        [SerializeField] private float _startAngle = 0f;

        [Tooltip("Направление расстановки: true - по часовой стрелке, false - против часовой стрелки.")]
        [SerializeField] private bool _clockwise = true;

        [Tooltip("Пересчитать позиции элементов при изменении размера")]
        [SerializeField] private bool _recalculateOnResize = true;

        private List<RectTransform> _items = new List<RectTransform>();
        private Vector2 _lastSize;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _lastSize = _rectTransform.sizeDelta;
        }

        private void Update()
        {
            if (_recalculateOnResize && _rectTransform.sizeDelta != _lastSize)
            {
                UpdateLayout();
                _lastSize = _rectTransform.sizeDelta;
            }
        }

        public void AddElement(RectTransform element)
        {
            _items.Add(element);
            UpdateLayout();
        }

        public void UpdateLayout()
        {
            if (_items == null || _items.Count == 0)
                return;

            float angleStep = 360f / _items.Count;
            float currentAngle = _startAngle;

            for (int i = 0; i < _items.Count; i++)
            {
                float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * _radius;
                float y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * _radius;

                _items[i].anchoredPosition = new Vector2(x, y);

                currentAngle += angleStep * (_clockwise ? -1 : 1);
            }
        }
    }
}
