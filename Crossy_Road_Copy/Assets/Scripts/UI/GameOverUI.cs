using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI BestScoreUI;

    private GameObject[] _childs;
    private Button _restartBtn;
    private Button _deleteDataBtn;
    private Button _loadTitleBtn;
    private int _childCount;
    private void Awake()
    {
        _childCount = transform.childCount;
        _childs = new GameObject[_childCount];
        
        for (int i = 0; i < _childCount; i++)
        {
            _childs[i] = transform.GetChild(i).gameObject;
        }
               
        _restartBtn = transform.Find("RestartButton").GetComponent<Button>();
        _loadTitleBtn = transform.Find("MainMenu").GetComponent<Button>();
        _deleteDataBtn = transform.Find("DeleteDataButton").GetComponent<Button>();

        _restartBtn.onClick.AddListener(Restart);
        _loadTitleBtn.onClick.AddListener(LoadTitle);
        _deleteDataBtn.onClick.AddListener(DeleteData);

        UpdateText(PlayerPrefs.GetInt("BestScore", 0));
    }

    public void Restart() =>         GameManager.Instance.Restart();

    public void LoadTitle() => GameManager.Instance.LoadTitle();

    public void DeleteData() => GameManager.Instance.DeleteData();

    public void Activate()
    {
        for (int i = 0; i < _childCount; i++)
        {
            _childs[i].SetActive(true);
        }
    }
    
    public void UpdateText(int bestScore) => BestScoreUI.text = $"{bestScore}";

    void OnEnable()
    {
        GameManager.Instance.OnGameOver.AddListener(Activate);
        GameManager.Instance.OnBestScoreChanged.AddListener(UpdateText);
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameOver.RemoveListener(Activate);
        GameManager.Instance.OnBestScoreChanged.RemoveListener(UpdateText);
        _restartBtn.onClick.RemoveListener(Restart);
        _loadTitleBtn.onClick.RemoveListener(LoadTitle);
        _deleteDataBtn.onClick.RemoveListener(DeleteData);
    }
}
