using Core;
using Core.View;
using UnityEngine;

namespace ItemGrabber
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
        }

        private void OnItemIsNotInRange(IAttachableView item)
        {
            if (_itemInRange == item)
                _itemInRange = null;
        }

        public IAttachableView Grab()
        {
            _attachable = _itemInRange;
            _animatorController.SetBool(AnimatorParameter.Grab, true);

            return _attachable;
        }

        public IAttachableView Drop()
        {
            var droped = _attachable;
            _attachable = null;
            _animatorController.SetBool(AnimatorParameter.Grab, false);

            return droped;
        }
    }
}
