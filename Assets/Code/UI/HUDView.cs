using TMPro;
using UnityEngine;

namespace Code.UI
{
    public sealed class HUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreCountText;
        [SerializeField] private TMP_Text _coinCountText;
        [SerializeField] private TMP_Text _timeText;
        
        public TMP_Text ScoreCountText  => _scoreCountText;
        public TMP_Text CoinCountText => _coinCountText;
        public TMP_Text TimeText => _timeText;
    }
}