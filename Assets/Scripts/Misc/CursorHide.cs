using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorHide : MonoBehaviour
{
    private Scene _currentScene;
    void Start()
    {

        _currentScene = SceneManager.GetActiveScene();

        if(_currentScene.name == "Game")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        
    }


}
