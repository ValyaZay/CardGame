using UnityEngine;
using UnityEngine.UI;

namespace ValZay.CardGame
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        
        private int score = 0;

        private void Start()
        {
            scoreText.text = score.ToString();
        }

        public void UpdateScore()
        {
            score += 1;
            scoreText.text = score.ToString();
        }
    }
}