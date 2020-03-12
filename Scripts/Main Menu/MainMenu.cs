using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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

}
