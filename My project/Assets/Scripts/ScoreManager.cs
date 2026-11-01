using TMPro;
using UnityEngine;

/// <summary>
/// Listens for target hits and adds the points to the score.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score;

    // event is static so always unsubscribe or it holds onto this after it's gone
    private void OnEnable()
    {
        Target.OnTargetHit += AddPoints;
    }

    private void OnDisable()
    {
        Target.OnTargetHit -= AddPoints;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}