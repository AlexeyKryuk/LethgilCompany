using Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Customization
{
    public class CustomizationUI : BaseUIElement
    {
        [SerializeField] private Button _left;
        [SerializeField] private Button _right;

        private SkinType _current;
        private int _lastSkinIndex;

        public event Action<SkinType> Selected;

        public void Initialize(SkinType skinType)
        {
            _lastSkinIndex = Enum.GetNames(typeof(SkinType)).Length - 1;
            _current = skinType;

            CheckLastIndexes();
        }

        private void OnEnable()
        {
            _left.onClick.AddListener(OnClickLeft);
            _right.onClick.AddListener(OnClickRight);
        }

        private void OnDisable()
        {
            _left.onClick.RemoveListener(OnClickLeft);
            _right.onClick.RemoveListener(OnClickRight);
        }

        private void OnClickLeft()
        {
            Selected?.Invoke(--_current);
            CheckLastIndexes();
        }

        private void OnClickRight()
        {
            Selected?.Invoke(++_current);
            CheckLastIndexes();
        }

        private void CheckLastIndexes()
        {
            if (_current == 0)
                _left.gameObject.SetActive(false);
            else
                _left.gameObject.SetActive(true);

            if (_current == (SkinType)_lastSkinIndex)
                _right.gameObject.SetActive(false);
            else
                _right.gameObject.SetActive(true);
        }
    }
}
