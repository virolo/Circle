using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Timer : MonoBehaviour
    {
        private const int SECOND = 1000;

        public event Action OnFinished;
        
        [SerializeField]
        private int _time;

        [SerializeField]
        private TextMeshProUGUI _timerMesh;

        private int _counter;

        public void Enable()
        {
            _timerMesh.enabled = true;
        }

        public void Disable()
        {
            _timerMesh.enabled = false;
        }
        
        public void StartCounter()
        {
            Enable();
            Count(_time);
        }

        private async void Count(int value)
        {
            _counter = value;
            UpdateTimerText();
            
            if (_counter == 0)
            {
                OnFinished?.Invoke();
            }
            else
            {
                await Task.Delay(SECOND);
                Count(_counter - 1);
            }
        }

        private void UpdateTimerText()
        {
            _timerMesh.text = _counter.ToString();
        }
    }
}