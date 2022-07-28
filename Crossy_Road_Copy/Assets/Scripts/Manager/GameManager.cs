using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //SingletonBehaviour<GameManager>
{
    public GameOverUI GameOverUI;
    public ScoreText Score;
    public CoinText Coin;
    public CamTarget CamTarget;

    private bool _isGameOver = false;
    public void Restart()
    {
        if (_isGameOver)
        {
            reset();
            SceneManager.LoadScene(1);
        }
    }
    
    private void reset()
    {
        _isGameOver = false;
    }

    public void End()
    {
        CamTarget.isOn = false;

        // 데이터 저장
        int savedBestScore = PlayerPrefs.GetInt("BestScore", 0);
        int bestScore = Mathf.Max((int)Score.score, savedBestScore);
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.SetInt("Coin", (int)Coin.player.coin);

        GameOverUI.Activate(bestScore);

        _isGameOver = true;
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(0);
    }
}
