using Core;
using Core.View;
using UnityEngine;

namespace ItemGrabbing
{
    public class PlayerItemGrabberView : MonoBehaviour, IGrabberView
    {
        [SerializeField] private Transform _anchor;

        private IAttachableView _itemInRange;
        private IAttachableView _attachable;

        private IPhysicsEventBroadcaster<AttachableItemView> _broadcaster;
        private IAnimatorController _animatorController;

        public Transform Anchor => _anchor;
        public bool IsGrabReady => _itemInRange != null;
        public bool IsGrabActive => _attachable != null;

        public Transform DropForward { get; private set; }

        private void Awake()
        {
            _broadcaster = GetComponentInChildren<IPhysicsEventBroadcaster<AttachableItemView>>();
            _animatorController = GetComponentInChildren<IAnimatorController>();
        }

        private void OnEnable()
        {
            _broadcaster.onTriggerEnter += OnItemInRange;
            _broadcaster.onTriggerExit += OnItemIsNotInRange;
        }

        private void OnDisable()
        {
            _broadcaster.onTriggerEnter -= OnItemInRange;
            _broadcaster.onTriggerExit -= OnItemIsNotInRange;
        }

        private void OnItemInRange(IAttachableView item)
        {
            _itemInRange = item;

            if (IsGrabActive == false)
                _itemInRange.Render(true);
        }

        private void OnItemIsNotInRange(IAttachableView item)
        {
            if (_itemInRange == item)
            {
                if (IsGrabActive == false)
                    _itemInRange.Render(false);

                _itemInRange = null;
            }
        }

        public IAttachableView Grab()
        {
            _itemInRange.Render(false);

            _attachable = _itemInRange;
            _animatorController.SetBool(AnimatorParameter.Grab, true);

            return _attachable;
        }

        public IAttachableView Drop()
        {
            _attachable.Render(true);

            var droped = _attachable;
            _attachable = null;
            _animatorController.SetBool(AnimatorParameter.Grab, false);

            return droped;
        }

        public void Initialize(Transform dropForward)
        {
            DropForward = dropForward;
        }
    }
}
