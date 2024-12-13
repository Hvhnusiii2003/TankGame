using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0;                     
    public int scoreThreshold = 100;
    public Sprite[] numberSprites;             
    public Image[] scoreDigits;

    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }
    public void UpdateScoreDisplay()
    {
        string scoreString = score.ToString("D3");

        for (int i = 0; i < scoreDigits.Length; i++)
        {
            if (i < scoreString.Length)
            {
                int digit = int.Parse(scoreString[i].ToString());
                scoreDigits[i].sprite = numberSprites[digit];
                scoreDigits[i].enabled = true;
            }
        }
    }

    public bool ShouldIncreaseSpawn(ref int enemiesPerSpawn, ref float spawnInterval)
    {
        if (score >= scoreThreshold)
        {
            enemiesPerSpawn++;
            spawnInterval = Mathf.Max(3f, spawnInterval - 0.1f);
            scoreThreshold += 100;
            return true;
        }
        return false;
    }
}
