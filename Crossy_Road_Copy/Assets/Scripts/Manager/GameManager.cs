using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent OnGameOver = new UnityEvent();
    public UnityEvent<int> OnBestScoreChanged = new UnityEvent<int>();
    public int BestScore
    {
        get
        {
            return _bestScore;
        }
        set
        {
            _bestScore = value;
            OnBestScoreChanged.Invoke(_bestScore);
        }
    }
    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
    public int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            OnScoreChanged.Invoke(_currentScore);
        }
    }
    public UnityEvent<int> OnCoinChanged = new UnityEvent<int>();
    public int CurrentCoin
    {
        get
        {
            return _currentCoin;
        }
        set
        {
            _currentCoin = value;
            OnCoinChanged.Invoke(_currentCoin);
        }
    }

    private int _bestScore = 0;
    private int _currentScore = 0;
    private int _currentCoin = 0;
    private bool _isGameOver = false;
    private void Start()
    {
        reset();
    }

    private void reset()
    {
        _isGameOver = false;
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        CurrentScore = 0;
        CurrentCoin = PlayerPrefs.GetInt("Coin", 0);
    }

    public void Restart()
    {
        if (_isGameOver)
        {
            reset();
            SceneManager.LoadScene(1);
        }
    }
    
    public void AddScore()
    {
        ++CurrentScore;
    }

    public void AddCoin()
    {
        ++CurrentCoin;
    }

    public void End()
    {

        // 데이터 저장
        BestScore = Mathf.Max(CurrentScore, BestScore);
        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("Coin", CurrentCoin);

        _isGameOver = true;
        OnGameOver.Invoke();
        // GameOverUI.Activate(bestScore);
    }

    public void DeleteData()
    {
        if(_isGameOver)
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void LoadTitle()
    {
        if (_isGameOver)
        {
            reset();
            SceneManager.LoadScene(0);
        }
    }
}
