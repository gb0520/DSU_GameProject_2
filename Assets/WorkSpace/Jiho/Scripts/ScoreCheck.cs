using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JH
{
    public class ScoreCheck : MonoBehaviour
    {
        [SerializeField] private BallState ball;
        [SerializeField] private Image scoreImage;
        [SerializeField] private Image percentImage;
        [SerializeField] private Image clearImage;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI percentText;

        private float currentScore;
        private float completeScore;
        private float scorePercent;

        public void OnScoreCheckUpdate()
        {
            currentScore = ball.CurrentScore;
            completeScore = ball.CompleteScore;

            scoreText.text = currentScore.ToString() + " / " + completeScore.ToString();
            scorePercent = currentScore / completeScore * 100;
            percentText.text = scorePercent.ToString() + "%";
        }

        public void OnScoreCheckClear()
        {
            if (currentScore >= completeScore)
                clearImage.gameObject.SetActive(true);
        }
    }
}

