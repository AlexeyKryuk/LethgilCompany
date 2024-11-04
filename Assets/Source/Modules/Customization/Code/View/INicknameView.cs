using Core;
using UnityEngine;

namespace Customization
{
    public interface INicknameView : IUIElement
    {
        void Initialize(string nickName);
        void Show(Transform camera);
        void Hide();
    }
}
