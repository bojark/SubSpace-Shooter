using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _highScoreText;
    [SerializeField]
    private Sprite[] _spriteList;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private WorldLogic _worldLogic;
    private GameObject _pauseMenu;
    private Animator _pauseMenuAnim;


    void Start()
    {

        _scoreText.text = "0";
        _gameOverText.gameObject.SetActive(false);

        _pauseMenu = GameObject.Find("Pause Panel");

        if(_pauseMenu == null)
        {
            Debug.LogError("Меню паузы не существует!");
        }
        else
        {  
            _pauseMenuAnim = _pauseMenu.GetComponent<Animator>();
            _pauseMenuAnim.updateMode = AnimatorUpdateMode.UnscaledTime; //анимация аниматора не зависит от масштаба времени
            _pauseMenu.SetActive(false);
        }
         
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        switch (_pauseMenu.activeInHierarchy)
        {
            case true:
                _pauseMenu.SetActive(false);
                _pauseMenuAnim.SetBool("IsPaused", false);
                Time.timeScale = 1;
                break;
            case false:
                _pauseMenu.SetActive(true);
                _pauseMenuAnim.SetBool("IsPaused", true);
                Time.timeScale = 0;
                break;
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void UpdateHighScore(int score)
    {
        _highScoreText.text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        _livesImg.sprite = _spriteList[lives];
        
        if(lives <= 0)
        {
            GameOver();
        }
    }

    IEnumerator gameOverCoroutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    void GameOver()
    {
        _worldLogic.IsGameOver = true;
        StartCoroutine(gameOverCoroutine());

    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        if (_worldLogic.IsCoop)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

}
