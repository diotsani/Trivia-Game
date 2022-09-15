using Dio.TriviaGame.Database;
using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Dio.TriviaGame.Gameplay
{
    public class Quiz : MonoBehaviour
    {
        private SaveData saveData = SaveData.saveDataInstance;
        private PackDatabase packDatabase = PackDatabase.databaseInstance;
        private QuizData quizData;
        [SerializeField] private QuizScriptable quizScriptable;
        [SerializeField] private List<QuizData> quizDataList;
        private List<string> listLevelId = new List<string>();
        //[SerializeField] private string _quizID;
        [SerializeField] private string selectedNameLevel;
        [SerializeField] private int selectedIndexLevel;

        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Image hintImage;
        [SerializeField] private string correctAnswer;
        [SerializeField] private string checkAnswer;
        [SerializeField] private int coinLevel;

        [SerializeField] private List<Button> answerButtonList;

        [SerializeField] private Button answerPrefab;
        [SerializeField] private Transform answerParent;

        private void OnEnable()
        {
            EventManager.StartListening("SetDataMessage", SetQuizData);
            EventManager.StartListening("StartGameMessage", InitQuiz);
        }
        private void OnDisable()
        {
            EventManager.StopListening("SetDataMessage", SetQuizData);
            EventManager.StopListening("StartGameMessage", InitQuiz);
        }
        private void Awake()
        {
            answerButtonList = new List<Button>();
            quizScriptable = PackDatabase.databaseInstance.levelPackSelected;
            selectedNameLevel = PackDatabase.databaseInstance.packName;
            selectedIndexLevel = PackDatabase.databaseInstance.levelIndex;
            //_quizID = quizScriptable.quizDataID;
            SetQuizData();
            SetLevelId();
        }
        void SetQuizData()
        {
            quizDataList = quizScriptable.quizData;
            quizData = quizDataList[selectedIndexLevel]; 
        }
        void SetLevelId()
        {
            for (int i = 0; i < quizDataList.Count; i++)
            {
                var q = quizDataList[i].QuizLevelID = selectedNameLevel + i;
                listLevelId.Add(q);
            }
            //for (int i = 0; i < quizDataList.Count; i++)
            //{
            //    var q = quizDataList[i].QuizLevelID;
            //    listLevelId.Add(q);
            //}
        }
        void InitQuiz()
        {
            questionText.text = quizData.question;
            correctAnswer = quizData.correctAnswer;
            hintImage.sprite = quizData.hintImage;
            coinLevel = quizData.coin;

            List<string> answerName = quizData.answerList;
            for (int i = 0; i < quizData.answerList.Count; i++)
            {
                if (answerButtonList.Count < quizData.answerList.Count)
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

            OnCheckAnswer();
        }
        void OnCheckAnswer()
        {
            if (correctAnswer == checkAnswer)
            {
                //saveData.AddLevelIdData(getNameLevelID,coinLevel);
                saveData.AddLevelIdData(quizDataList[selectedIndexLevel-1].QuizLevelID, coinLevel);
                EventManager.TriggerEvent("PlayerWinMessage", new PlayerWinMessage(selectedNameLevel,selectedIndexLevel));
                EventManager.TriggerEvent("StopCountdownMessage");
                CheckNextLevel();
            }
            else
                EventManager.TriggerEvent("GoToLevelMessage");
        }
        void CheckNextLevel()
        {
            GetQuizData();
            if (selectedIndexLevel < quizDataList.Count)
            {
                EventManager.TriggerEvent("NextLevelMessage");
            }
            else
            {
                EventManager.TriggerEvent("GoToPackMessage");
            }
            
        }
        void GetQuizData()
        {
            //saveData.AddQuizIdData(_quizID);'
            //saveData.AddQuizIdData(quizScriptable.quizDataID);
            //saveData.AddPackIdData(selectedNameLevel);
            
            

        }
    }
}