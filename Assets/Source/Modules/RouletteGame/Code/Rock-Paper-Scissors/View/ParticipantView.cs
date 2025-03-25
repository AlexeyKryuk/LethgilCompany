using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace RockPaperScissors
{
    public class ParticipantView : MonoBehaviourPunCallbacks
    {
        private const string CHOICE = nameof(CHOICE);
        private const string NICKNAME = nameof(NICKNAME);

        public event Action<string, Choice> ChoiceChanged;

        public void OnSelectChoice(Choice choice)
        {
            byte[] serializedData = SerializationHelper.Serialize(choice);

            Hashtable props = new Hashtable
            {
                {CHOICE, serializedData},
                {NICKNAME, PhotonNetwork.LocalPlayer.NickName},
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.ContainsKey(CHOICE))
            {
                byte[] serializedData = (byte[])changedProps[CHOICE];
                Choice receivedData = (Choice)SerializationHelper.Deserialize(serializedData);

                if (changedProps.TryGetValue(NICKNAME, out object nickName))
                {
                    if (PhotonNetwork.IsMasterClient)
                        ChoiceChanged?.Invoke(nickName.ToString(), receivedData);
                }
            }
        }
    }
}
