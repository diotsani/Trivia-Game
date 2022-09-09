using Dio.TriviaGame.Database;
using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dio.TriviaGame.Gameplay
{
    public class Quiz : MonoBehaviour
    {
        private QuizData quizData;
        [SerializeField] private QuizScriptable quizScriptable;
        [SerializeField] private List<QuizData> quizDataList;
        [SerializeField] private string selectedNameLevel;
        [SerializeField] private int selectedIndexLevel;
        private int _amountAnswer = 4;

        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Image hintImage;
        [SerializeField] private string correctAnswer;
        [SerializeField] private string checkAnswer;

        [SerializeField] private List<Button> answerButtonList;

        [SerializeField] private Button answerPrefab;
        [SerializeField] private Transform answerParent;
        private void OnEnable()
        {
            EventManager.StartListening("SetDataMessage", SetQuizData);
            EventManager.StartListening("StartGameMessage", NewQuiz);
        }
        private void OnDisable()
        {
            EventManager.StopListening("SetDataMessage", SetQuizData);
            EventManager.StopListening("StartGameMessage", NewQuiz);
        }
        private void Awake()
        {
            answerButtonList = new List<Button>();
            quizScriptable = PackDatabase.databaseInstance.levelPackSelected;
            selectedIndexLevel = PackDatabase.databaseInstance.levelIndex;
            selectedNameLevel = PackDatabase.databaseInstance.packName;
            SetQuizData();
        }
        void SetQuizData()
        {
            quizDataList = quizScriptable.quizData;
            quizData = quizDataList[selectedIndexLevel];
        }
        void NewQuiz()
        {
            InitQuiz(quizData);
        }
        void InitQuiz(QuizData quiz)
        {
            questionText.text = quiz.question;
            correctAnswer = quiz.correctAnswer;
            hintImage.sprite = quiz.hintImage;

            List<string> answerName = quiz.answerList;
            for (int i = 0; i < quiz.answerList.Count; i++)
            {
                if (answerButtonList.Count < quiz.answerList.Count)
                {
                    Button answerButton = Instantiate(answerPrefab, answerParent);
                    answerButtonList.Add(answerButton);
                    answerButton.name = answerName[i];
                    answerButton.GetComponent<AnswerObject>()._answerText.text = answerName[i];
                    answerButton.GetComponent<AnswerObject>().answerToCheck = answerName[i];

                    answerButton.onClick.RemoveAllListeners();
                    answerButton.onClick.AddListener(() => OnClickAnswer(answerButton));
                }
                else
                {
                    for (int j = 0; j < answerButtonList.Count; j++)
                    {
                        Button newButton = answerButtonList[j];
                        newButton.name = answerName[j];
                        newButton.GetComponent<AnswerObject>()._answerText.text = answerName[j];
                        newButton.GetComponent<AnswerObject>().answerToCheck = answerName[j];

                        newButton.onClick.RemoveAllListeners();
                        newButton.onClick.AddListener(() => OnClickAnswer(newButton));
                    }
                    
                }
            }
            selectedIndexLevel++;
        }

        void OnClickAnswer(Button button)
        {
            var b = button.GetComponent<AnswerObject>();
            checkAnswer = b.answerToCheck;

            OnCheckAnswer(quizData);
        }
        void OnCheckAnswer(QuizData quiz)
        {
            if (correctAnswer == checkAnswer)
            {
                quiz.isComplete = true;
                SaveData.saveDataInstance.Save();
                EventManager.TriggerEvent("PlayerWinMessage", new PlayerWinMessage(selectedNameLevel,selectedIndexLevel));
                EventManager.TriggerEvent("StopCountdownMessage");
                CheckNextLevel();
            }
            else
                EventManager.TriggerEvent("GoToLevelMessage");
        }
        void CheckNextLevel()
        {
            if (selectedIndexLevel < quizDataList.Count)
            {
                EventManager.TriggerEvent("NextLevelMessage");
            }
            else
            {
                EventManager.TriggerEvent("GoToPackMessage");
                Debug.Log("All Level Completed");
            }
        }
    }
}