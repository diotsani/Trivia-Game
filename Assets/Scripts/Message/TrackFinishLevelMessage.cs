namespace Dio.TriviaGame.Message
{
    public struct TrackFinishLevelMessage
    {
        public string levelName;
        public int indexLevel;

        public TrackFinishLevelMessage(string name, int index)
        {
            levelName = name;
            indexLevel = index;
        }
    }
}