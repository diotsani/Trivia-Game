using Dio.TriviaGame.Database;
using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dio.TriviaGame.Gameplay
{
    public class Quiz : MonoBehaviour
    {
        private SaveData _saveData = SaveData.saveDataInstance;
        private PackDatabase _packDatabase = PackDatabase.databaseInstance;
        private QuizData _quizData;
        private QuizScriptable _quizScriptable;
        private List<QuizData> _quizDataList;
        private string _namePack;
        private int _indexLevel;

        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private Image _hintImage;
        private string _correctAnswer;
        private string _checkAnswer;
        private int _coinLevel;

        private List<Button> answerButtonList;

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
            _quizDataList = new List<QuizData>();

            _quizScriptable = PackDatabase.databaseInstance.levelPackSelected;
            _namePack = PackDatabase.databaseInstance.packName;
            _indexLevel = PackDatabase.databaseInstance.indexLevel;

            SetQuizData();
        }
        void SetQuizData()
        {
            _quizDataList = _quizScriptable.quizData;
            _quizData = _quizDataList[_indexLevel];

            for (int i = 0; i < _quizDataList.Count; i++)
            {
                _quizDataList[i].QuizLevelID = _namePack + i;
            }
        }
        void InitQuiz()
        {
            _questionText.text = _quizData.question;
            _correctAnswer = _quizData.correctAnswer;
            _hintImage.sprite = _quizData.hintImage;
            _coinLevel = _quizData.coin;

            List<string> answerName = _quizData.answerList;
            for (int i = 0; i < _quizData.answerList.Count; i++)
            {
                if (answerButtonList.Count < _quizData.answerList.Count)
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
            _indexLevel++;
        }
        void OnClickAnswer(Button button)
        {
            AnswerObject obj = button.GetComponent<AnswerObject>();
            _checkAnswer = obj.answerToCheck;

            OnCheckAnswer();
        }
        void OnCheckAnswer()
        {
            if (_correctAnswer == _checkAnswer)
            {
                string data = _quizDataList[_indexLevel - 1].QuizLevelID;

                _saveData.AddLevelIdData(data, _coinLevel);
                _saveData.AddProgressLevelData(data);

                EventManager.TriggerEvent("PlayerWinMessage", new PlayerWinMessage(_namePack,_indexLevel));
                EventManager.TriggerEvent("StopCountdownMessage");
                CheckNextLevel();
            }
            else
                EventManager.TriggerEvent("GoToLevelMessage");
        }
        void CheckNextLevel()
        {
            _saveData.CheckProgressLevel(_quizDataList,_namePack);

            if (_indexLevel < _quizDataList.Count)
            {
                EventManager.TriggerEvent("NextLevelMessage");
            }
            else
            {
                EventManager.TriggerEvent("GoToPackMessage");
            }
        }
    }
}