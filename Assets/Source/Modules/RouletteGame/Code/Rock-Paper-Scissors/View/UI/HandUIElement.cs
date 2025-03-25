using Core;
using System;
using System.Linq;
using UnityEngine;

namespace RockPaperScissors
{
    public class HandUIElement : BaseUIElement
    {
        [SerializeField] private ChoiceUIElement[] _choiceElements;

        public ChoiceType Choice { get; private set; }

        public event Action<ChoiceType> Selected;

        public void Initialize(RPSGameConfig config)
        {
            foreach (var element in _choiceElements)
            {
                var data = config.Choices.FirstOrDefault(item
                    => item.ChoiceType == element.ChoiceType);

                element.Initialize(data);
            }
        }

        private void OnEnable()
        {
            foreach (var element in _choiceElements)
                element.Selected += OnChoiceSelect;
        }

        private void OnDisable()
        {
            foreach (var element in _choiceElements)
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
            foreach (var element in _choiceElements)
                if (element != choiceUI)
                    element.Deselect();

            choiceUI.Select();
        }
    }
}
