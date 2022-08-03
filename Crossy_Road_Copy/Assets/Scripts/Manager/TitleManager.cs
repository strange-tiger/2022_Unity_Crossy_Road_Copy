using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }

    //public void LoadShop()
    //{
    //    SceneManager.LoadScene(2);
    //}
}
