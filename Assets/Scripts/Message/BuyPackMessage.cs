
using Dio.TriviaGame.Pack;

namespace Dio.TriviaGame.Message
{
    public struct BuyPackMessage
    {
        public int spend;
        public PackObject pack;
        public int idPack;
        public BuyPackMessage(int get, PackObject Pack, int IdPack)
        {
            spend = get;
            pack = Pack;
            idPack = IdPack;    
        }
    }
}