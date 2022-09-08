namespace Dio.TriviaGame.Message
{
    public struct PlayerWinMessage
    {
        public string levelName;
        public int indexLevel;

        public PlayerWinMessage(string name, int index)
        {
            levelName = name;
            indexLevel = index;
        }
    }
}