using Customization;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Network
{
    public class NickNameView : MonoBehaviourPunCallbacks, INicknameView
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private float _timeToFade;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _alphaSpeed;

        private Color _showColor;
        private Color _hideColor;
        private float _elapsedTime;
        private Transform _camera;

        private bool _isShowing;

        public void Initialize(string nickName)
        {
            if (_photonView.IsMine)
                return;

            _text.text = nickName;
        }

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        private void Awake()
        {
            _showColor = _text.color;
            _showColor.a = 1f;

            _hideColor = _text.color;
            _hideColor.a = 0f;
        }

        private void Update()
        {
            if (_isShowing)
            {
                _text.color = Color.Lerp(_text.color, _showColor, Time.deltaTime * _alphaSpeed);
                _elapsedTime = 0f;
            }
            else
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _timeToFade)
                {
                    _text.color = Color.Lerp(_text.color, _hideColor, Time.deltaTime * _alphaSpeed);
                    _elapsedTime = _timeToFade;
                }
            }

            if (_camera != null)
                transform.LookAt(_camera.position);
        }

        public void Show(Transform camera)
        {
            _camera = camera;
            _isShowing = true;
        }

        public void Hide()
        {
            _isShowing = false;
        }
    }
}
