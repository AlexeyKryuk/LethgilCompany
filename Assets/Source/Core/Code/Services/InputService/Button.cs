using System;

namespace Core
{
    public class Button
    {
        public event Action PointerDown;
        public event Action<float> PointerUp;

        public float HoldValue { get; private set; }
        public bool IsPressed { get; private set; }

        public void Click(bool isPressed)
        {
            IsPressed = isPressed;

            if (IsPressed)
                OnPointerDown();
            else
                OnPointerUp();
        }

        public void Update(float deltaTime)
        {
            if (IsPressed)
                HoldValue += deltaTime;
        }

        private void OnPointerDown()
        {
            IsPressed = true;
            PointerDown?.Invoke();
            HoldValue = 0;
        }

        private void OnPointerUp()
        {
            IsPressed = false;
            PointerUp?.Invoke(HoldValue);
            HoldValue = 0;
        }
    }
}
