using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RockPaperScissors
{
    public class HandUIElement : BaseUIElement
    {
        [SerializeField] private Transform _parent;

        private List<ChoiceUIElement> _choices = new List<ChoiceUIElement>();
        private CircleLayout _circleLayout;

        public ChoiceType Choice { get; private set; }

        public event Action<ChoiceType> Selected;

        public void Initialize(RPSGameConfig config)
        {
            _circleLayout = GetComponent<CircleLayout>();

            foreach (var data in config.Choices)
            {
                var element = Instantiate(config.ChoiceUIPrefab, _parent);

                element.Initialize(data);
                element.Selected += OnChoiceSelect;

                _choices.Add(element);
                _circleLayout.AddElement(element.GetComponent<RectTransform>());
            }
        }

        private void OnDisable()
        {
            foreach (var element in _choices)
                element.Selected -= OnChoiceSelect;
        }

        private void OnChoiceSelect(ChoiceUIElement choiceUI)
        {
            RenderUI(choiceUI);

            Choice = choiceUI.ChoiceType;
            Selected?.Invoke(Choice);
        }

        private void RenderUI(ChoiceUIElement choiceUI)
        {
            foreach (var element in _choices)
                if (element != choiceUI)
                    element.Deselect();

            choiceUI.Select();
        }
    }
}
