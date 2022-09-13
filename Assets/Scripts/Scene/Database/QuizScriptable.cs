using Dio.TriviaGame.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dio.TriviaGame.Database
{
    [CreateAssetMenu(fileName = "QuizScriptable", menuName = "DataScriptable")]
    public class QuizScriptable : ScriptableObject
    {
        public string quizDataID = "Quiz";
        public List<QuizData> quizData;
    }
}