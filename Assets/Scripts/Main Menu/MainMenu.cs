using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text _highScoreText;

    public void Start()
    {

        if (_highScoreText == null)
        {
            Debug.LogError("Хайскортекста не существует");
        }
        else
        {
            _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }


    }
    public void NewGame()
    {
        SceneManager.LoadScene(1); // Game Scene
    }

    public void Coop()
    {
        SceneManager.LoadScene(2); // Game Scene
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        Debug.LogWarning("High Score обнулён!");
        _highScoreText.text = "0";
    }

}
