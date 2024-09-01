using Core;
using UnityEngine;

namespace ItemGrabber
{
    public class GrabbingService : IGrabbingService
    {
        private readonly IInputService _inputService;

        private IGrabberView _grabber;

        public GrabbingService(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(IGrabberView grabber)
        {
            _grabber = grabber;
        }

        public void Tick()
        {
            if (_inputService.CharacterInputs.ActionButton)
            {
                if (_grabber.IsGrabActive)
                {
                    var droped = _grabber.Drop();
                    droped.Drop();
                }
                else if (_grabber.IsGrabReady)
                {
                    var item = _grabber.Grab();
                    item.Attach(_grabber);
                }

                Debug.Log(_grabber.IsGrabActive);
                Debug.Log(_grabber.IsGrabReady);

                _inputService.CharacterInputs.ActionButton = false;
            }
        }
    }
}
