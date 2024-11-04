using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Customization
{
    public class CustomizationView : MonoBehaviour, ICustomizationView
    {
        [SerializeField] private List<MeshRendererReference> _skins;

        public void Set(CustomizationInfo data)
        {
            foreach (var skin in _skins)
            {
                if (skin.Skin == data.Skin)
                    skin.Mesh.gameObject.SetActive(true);
                else
                    skin.Mesh.gameObject.SetActive(false);
            }
        }

        public SkinType GetCurrentActiveSkin()
            => _skins.FirstOrDefault((skin) => skin.Mesh.gameObject.activeInHierarchy).Skin;
    }

    [Serializable]
    public class MeshRendererReference
    {
        public SkinType Skin;
        public SkinnedMeshRenderer Mesh;
    }
}
