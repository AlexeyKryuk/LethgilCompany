using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class TimerView
    {
        private ITimer _model;
        private ICoroutineHandler _coroutineHandler;

        public TimerView(ITimer model, ICoroutineHandler coroutineHandler)
        {
            _model = model;
            _coroutineHandler = coroutineHandler;
        }

        public void StartTimer(Action onTimeOver)
        {
            _model.Start();
            _coroutineHandler.StartCoroutine(Tick(onTimeOver));
        }

        private IEnumerator Tick(Action onTimeOver)
        {
            while (_model.IsOver == false)
            {
                _model.Tick(Time.deltaTime);
                yield return null;
            }

            onTimeOver?.Invoke();
        }
    }
}
