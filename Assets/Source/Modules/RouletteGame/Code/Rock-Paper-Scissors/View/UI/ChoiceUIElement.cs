using Core;
using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace RockPaperScissors
{
    public class ChoiceUIElement : BaseUIElement
    {
        [SerializeField] private ChoiceType _choiceType;
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;

        private Color _selectedColor;
        private Color _defaultColor;

        public bool IsSelected { get; private set; }
        public ChoiceType ChoiceType => _choiceType;

        public event Action<ChoiceUIElement> Selected;

        public void Initialize(ChoiceStaticData staticData)
        {
            _defaultColor = _background.color;
            _selectedColor = staticData.SelectedColor;
            _icon.sprite = staticData.Icon;
        }

        public void Select()
        {
            _background.color = _selectedColor;
            IsSelected = true;
        }

        public void Deselect()
        {
            _background.color = _defaultColor;
            IsSelected = false;
        }

        private void OnEnable() => _button.onClick.AddListener(OnSelect);
        private void OnDisable() => _button.onClick.RemoveListener(OnSelect);
        private void OnSelect() => Selected?.Invoke(this);
    }
}
