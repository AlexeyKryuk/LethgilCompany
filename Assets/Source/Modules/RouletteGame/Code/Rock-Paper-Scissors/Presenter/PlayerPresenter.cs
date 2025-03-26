using Core;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace RockPaperScissors
{
    public class PlayerPresenter : ILifetimeCycleService, IInitializable, IStartable, IDisposable
    {
        private readonly IPlayerPresenter _mainPlayerPresenter;
        private readonly IUIService _uiService;
        private readonly RPSGameConfig _gameConfig;

        private List<Participant> _participants;
        private ParticipantView _view;
        private PlayerHUD _playerHUD;
        private Rules _rules;

        public PlayerPresenter(IPlayerPresenter playerPresenter, IUIService uIService, RPSGameConfig gameConfig)
        {
            _mainPlayerPresenter = playerPresenter;
            _uiService = uIService;
            _gameConfig = gameConfig;
        }

        public void Initialize()
        {
            _view = _mainPlayerPresenter.GetView<ParticipantView>();

            if (PhotonNetwork.IsMasterClient)
            {
                _participants = new List<Participant>();

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    Debug.Log(player.NickName);
                    _participants.Add(new Participant(player.NickName));
                }    
            }

            _playerHUD = _uiService.CreateUIElement<PlayerHUD>();
            _playerHUD.Initialize(_gameConfig);
        }

        public void Start()
        {
            _view.ChoiceChanged += OnChoiceChange;
            _playerHUD.Selected += _view.OnSelectChoice;
        }

        public void Dispose()
        {
            _view.ChoiceChanged -= OnChoiceChange;
            _playerHUD.Selected -= _view.OnSelectChoice;
        }

        private void OnChoiceChange(string nickName, Choice choice)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _participants.FirstOrDefault(participant => participant.Name == nickName).Pick(choice);

                foreach (var participant in _participants)
                    if (participant.IsReady == false)
                        return;

                _rules = new Rules(PhotonNetwork.PlayerList.Length);
                _rules.Play(_participants);

                LogResult();
            }
        }

        private void LogResult()
        {
            foreach (var player in _participants)
                Debug.Log(player.Name + ": " + player.Points);
        }
    }
}
