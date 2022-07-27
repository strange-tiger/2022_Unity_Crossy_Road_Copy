using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public ScoreText Score;
    public CoinText Coin;
    public GameOverUI GameOverUI;
    //public Button RestartButton;

    private bool _isOver = false;

    public void Restart()
    {
        if (_isOver)
        {
            _isOver = false;

            SceneManager.LoadScene(0);
        }
    }

    public void End()
    {
        // Ÿ�̸� ����
        Score.isOn = false;
        Coin.isOn = false;

        // ������ ����
        int savedBestScore = PlayerPrefs.GetInt("BestScore", 0);
        int bestScore = Mathf.Max((int)Score.score, savedBestScore);

        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.SetInt("Coin", (int)Coin.coin);

        // GameOverUI�� ����
        GameOverUI.Activate(bestScore);

        // _isOver = true;
        _isOver = true;
    }
}
