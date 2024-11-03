using System;
using UnityEngine;

namespace Customization
{
    [Serializable]
    public class CustomizationInfo
    {
        [SerializeField] private string _nickName;
        [SerializeField] private string _networkID;
        [SerializeField] private SkinType _skin;

        public CustomizationInfo()
        {
            _nickName = "Unknown";
            _networkID = "Incorrect";
        }

        public string NickName => _nickName;
        public string NetworkID => _networkID;
        public SkinType Skin => _skin;

        public void SetSkin(SkinType skin)
        {
            _skin = skin;
        }
    }
}
