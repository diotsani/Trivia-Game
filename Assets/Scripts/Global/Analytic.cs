using Dio.TriviaGame.Gameplay;
using Dio.TriviaGame.Message;
using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Global
{
    public class Analytic : MonoBehaviour
    {
        public static Analytic analytic;
        private void Awake()
        {
            if (analytic == null)
            {
                analytic = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        private void OnEnable()
        {
            EventManager.StartListening("TrackFinishLevelMessage", TrackFinishLevel);
        }
        private void OnDisable()
        {
            EventManager.StopListening("TrackFinishLevelMessage", TrackFinishLevel);
        }
        public void TrackFinishLevel(object windata)
        {
            TrackFinishLevelMessage message = (TrackFinishLevelMessage)windata;
            string nameLv = message.levelName;
            int indexLv = message.indexLevel;

            Debug.Log("Analytic Event (Level " + nameLv + "-" + indexLv + " have been completed)");

        }
        public void TrackUnlockLevel()
        {

        }
    }
}