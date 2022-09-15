using System.Collections.Generic;
using UnityEngine;

namespace Dio.TriviaGame.Gameplay
{
    [System.Serializable]
    public class QuizData
    {
        public string QuizLevelID;
        public string question;
        public Sprite hintImage;
        public List<string> answerList;
        public string correctAnswer;
        public int coin = 20;
        public QuizData()
        {
            QuizLevelID = null;
        }
    }
}