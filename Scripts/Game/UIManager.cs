using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _spriteList;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private WorldLogic _worldLogic;
    

    void Start()
    {
        _scoreText.text = "0";
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
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

}
