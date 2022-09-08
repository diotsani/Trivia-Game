
namespace Dio.TriviaGame.Message
{
    public struct BuyPackMessage
    {
        public int spend;
        public BuyPackMessage(int get)
        {
            spend = get;
        }
    }
}