using Core;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
    public class PlayerHUD : BaseUIElement
    {
        [SerializeField] private HandUIElement[] _hands;

        public event Action<Choice> Selected;

        public void Initialize(RPSGameConfig config)
        {
            foreach (var hand in _hands)
                hand.Initialize(config);
        }

        private void OnEnable()
        {
            foreach (var hand in _hands)
                hand.Selected += OnChoiceSelect;
        }

        private void OnDisable()
        {
            foreach (var hand in _hands)
                hand.Selected -= OnChoiceSelect;
        }

        private void OnChoiceSelect(ChoiceType choice)
        {
            List<ChoiceType> choices = new List<ChoiceType>();

            foreach (var hand in _hands)
            {
                if (hand.Choice == ChoiceType.None)
                    return;

                choices.Add(hand.Choice);
            }

            Selected?.Invoke(new Choice(choices.ToArray()));
        }
    }
}
