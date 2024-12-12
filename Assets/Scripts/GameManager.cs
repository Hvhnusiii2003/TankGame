using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0;                      // Điểm hiện tại
    public int scoreThreshold = 100;
    public Sprite[] numberSprites;             // Các sprite số từ 0-9
    public Image[] scoreDigits;                // Các Image để hiển thị chữ số (hàng trăm, hàng chục, hàng đơn vị)

    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // Hiển thị điểm số mặc định là 000
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        score += points; // Cộng điểm
        UpdateScoreDisplay();
    }
    public void UpdateScoreDisplay() // Đổi từ private thành public
    {
        string scoreString = score.ToString("D3"); // Định dạng thành chuỗi 3 chữ số, ví dụ: 000, 010, 100

        for (int i = 0; i < scoreDigits.Length; i++)
        {
            if (i < scoreString.Length)
            {
                int digit = int.Parse(scoreString[i].ToString()); // Lấy từng chữ số
                scoreDigits[i].sprite = numberSprites[digit];    // Gán sprite tương ứng
                scoreDigits[i].enabled = true;                   // Hiển thị chữ số
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
