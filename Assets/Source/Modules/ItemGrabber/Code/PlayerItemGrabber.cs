using Core;
using Core.View;
using System;
using UnityEngine;

namespace ItemGrabber
{
    public class PlayerItemGrabber : MonoBehaviour, IGrabberView
    {
        [SerializeField] private Transform _anchor;

        private IAttachable _itemInRange;
        private IAttachable _attachable;

        private IPhysicsEventBroadcaster<AttachableItem> _broadcaster;

        public Transform Anchor => _anchor;
        public bool IsGrabReady => _itemInRange != null;
        public bool IsGrabActive => _attachable != null;

        private void Awake()
            => _broadcaster = GetComponentInChildren<IPhysicsEventBroadcaster<AttachableItem>>();

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

        private void OnItemInRange(IAttachable item)
        {
            _itemInRange = item;
        }

        private void OnItemIsNotInRange(IAttachable item)
        {
            if (_itemInRange == item)
                _itemInRange = null;
        }

        public IAttachable Grab()
        {
            _attachable = _itemInRange;

            return _attachable;
        }

        public IAttachable Drop()
        {
            var droped = _attachable;
            _attachable = null;

            return droped;
        }
    }
}
