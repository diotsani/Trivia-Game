using Dio.TriviaGame.Global;
using TMPro;
using UnityEngine;

namespace Dio.TriviaGame.Gameplay
{
    public class Countdown : MonoBehaviour, IStartGame
    {
        [SerializeField] private TMP_Text timerText;
        private float _timeLeft;
        [SerializeField] private bool isPlayGame;
        public void OnEnable()
        {
            EventManager.StartListening("StopCountdownMessage",StopCountdown);
        }
        public void OnDisable()
        {
            EventManager.StopListening("StopCountdownMessage", StopCountdown);
        }
        private void Update()
        {
            if (isPlayGame)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    UpdateTimer(_timeLeft);
                }
                else
                {
                    FinishCountdown();

                }
            }
        }
        void UpdateTimer(float time)
        {
            time += 1;
            string seconds = (time % 60).ToString("f1");
            timerText.text = seconds;
        }

        public void StartCountdown()
        {
            _timeLeft = 30;
            isPlayGame = true;
            Time.timeScale = 1;
        }
        public void StopCountdown()
        {
            isPlayGame = false;
        }
        public void FinishCountdown()
        {
            _timeLeft = 0;
            isPlayGame = false;
            EventManager.TriggerEvent("TimeOutMessage");
        }
        public void OnStartGame()
        {
            StartCountdown();
        }
    }
}