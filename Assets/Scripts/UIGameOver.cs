using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameIver : MonoBehaviour
{
    public GameObject GameOverScene; // Tham chiếu đến Canvas cần ẩn

    void Start()
    {
        GameOverScene.SetActive(false);
    }

    public virtual void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public virtual void BackToMainMenu()
    {
        Time.timeScale = 1; // Khôi phục thời gian game
        SceneManager.LoadScene("MainMenu"); // Chuyển về cảnh Main Menu
    }
}
