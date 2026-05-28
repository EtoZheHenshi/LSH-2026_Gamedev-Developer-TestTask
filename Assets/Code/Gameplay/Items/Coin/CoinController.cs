using Code.Core.Events;

namespace Code.Gameplay.Items.Coin
{
    public sealed class CoinController
    {
        public static void Initialize(CoinView coinView, EventBus eventBus)
        {
            CoinModel coin = new CoinModel(eventBus);
            coinView.OnCollect += coin.CoinCollect;
        }  
    }
}