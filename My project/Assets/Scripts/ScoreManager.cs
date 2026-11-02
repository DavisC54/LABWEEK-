using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

/// <summary>
/// Listens for target hits and adds the points to the score. Also saves and loads the score.
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

    // score gets its own binary file, positions go in the json
    public void SaveScore(string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = File.Create(path))
        {
            formatter.Serialize(stream, score);
        }
    }

    public void LoadScore(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("no score file");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = File.OpenRead(path))
        {
            score = (int)formatter.Deserialize(stream);
        }

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