using System.Collections;
using UnityEngine;
using System.Linq;
using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;

namespace Dio.TriviaGame.Gameplay
{
    public class GameFlow : MonoBehaviour
    {
        [SerializeField] private GameObject countdown;
        private string nameLv;
        private int indexLv;
        private void Start()
        {
            StartGame();
        }

        private void OnEnable()
        {
            EventManager.StartListening("NextLevelMessage", InvokeNextLevel);
            EventManager.StartListening("TimeOutMessage", TimeOut);
            EventManager.StartListening("PlayerWinMessage", AnswerQuestion);
        }
        private void OnDisable()
        {
            EventManager.StopListening("NextLevelMessage", InvokeNextLevel);
            EventManager.StopListening("TimeOutMessage", TimeOut);
            EventManager.StopListening("PlayerWinMessage", AnswerQuestion);
        }
        public void AnswerQuestion(object windata)
        {
            PlayerWinMessage message = (PlayerWinMessage)windata;
            nameLv = message.levelName;
            indexLv = message.indexLevel;

            EventManager.TriggerEvent("TrackFinishLevelMessage", new TrackFinishLevelMessage(nameLv,indexLv));

        }
        public void TimeOut()
        {
            EventManager.TriggerEvent("GoToLevelMessage");
        }

        void InvokeNextLevel()
        {
            SetDataQuestion();
            Invoke("StartGame", 0.5f);
        }

        void StartGame()
        {
            IStartGame start = countdown.GetComponent<IStartGame>();
            start?.OnStartGame();

            EventManager.TriggerEvent("StartGameMessage");
        }
        void SetDataQuestion()
        {
            EventManager.TriggerEvent("SetDataMessage");
        }

    }
}