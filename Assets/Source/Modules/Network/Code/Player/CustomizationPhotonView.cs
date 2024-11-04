using Customization;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;

namespace Network
{
    public class CustomizationPhotonView : MonoBehaviourPunCallbacks, ICustomizationView
    {
        private const string SKIN_KEY = nameof(SKIN_KEY);
        private const string NET_ID = nameof(NET_ID);
        private const string NICKNAME = nameof(NICKNAME);

        [SerializeField] private List<MeshRendererReference> _skins;
        [SerializeField] private NickNameView _nickNameView;
        [SerializeField] private PhotonView _photonView;

        public INicknameView NickNameView => _nickNameView;

        public void Set(CustomizationInfo data)
        {
            Hashtable props = new Hashtable
            {
                {SKIN_KEY, data.Skin},
                {NICKNAME, data.NickName},
                {NET_ID, _photonView.ViewID}
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.TryGetValue(SKIN_KEY, out SkinType type))
            {
                if (changedProps.TryGetValue(NET_ID, out int id))
                {
                    if (PhotonNetwork.GetPhotonView(id).TryGetComponent(out CustomizationPhotonView component))
                    {
                        component.SetSkin(type);

                        if (_photonView.IsMine == false && changedProps.TryGetValue(NICKNAME, out object nickname))
                        {
                            component.NickNameView.Initialize(nickname.ToString());
                        }
                    }
                }
            }
        }

        private void SetSkin(SkinType type)
        {
            foreach (var skin in _skins)
            {
                if (skin.Skin == type)
                    skin.Mesh.gameObject.SetActive(true);
                else
                    skin.Mesh.gameObject.SetActive(false);
            }
        }
    }
}
