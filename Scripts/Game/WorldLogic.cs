using UnityEngine;
using UnityEngine.SceneManagement;


public class WorldLogic : MonoBehaviour
{
    [SerializeField]
    private float _borderY = 5.5f;
    [SerializeField]
    private float _borderX = 8.45f;
    [SerializeField]
    private float _playerBorderX = 9.8f;
    [SerializeField]
    private float _playerBorderY = -4.5f;
    [SerializeField]
    private bool _isActive = true;
    [SerializeField]
    private float _enemySpawnRate = 5.0f;
    [SerializeField]
    private float _destroyHeight = 8.0f;
    [SerializeField]
    private float _spaceSpeed = 1f;
    private int _spaceNum = 1;
    [SerializeField]
    private float _asteroidSpeed = 1f;
    [SerializeField]
    private Player _player;
    private bool _isGameOver = false;
    [SerializeField]
    private bool _isCoop;
    [SerializeField]
    private UIManager _uiManager;
    private int _score = 0;

    public float BorderY { get => _borderY; set => _borderY = value; }
    public float BorderX { get => _borderX; set => _borderX = value; }
    public float PlayerBorderX { get => _playerBorderX; set => _playerBorderX = value; }
    public float PlayerBorderY { get => _playerBorderY; set => _playerBorderY = value; }
    public bool IsActive { get => _isActive; set => _isActive = value; }
    public float DestroyHeight { get => _destroyHeight; set => _destroyHeight = value; }
    public float EnemySpawnRate { get => _enemySpawnRate; set => _enemySpawnRate = value; }
    public float SpaceSpeed { get => _spaceSpeed; set => _spaceSpeed = value; }
    public int SpaceNum { get => _spaceNum; set => _spaceNum = value; }
    public Player Player { get => _player; set => _player = value; }
    public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }
    public float AsteroidSpeed { get => _asteroidSpeed; set => _asteroidSpeed = value; }
    public bool IsCoop { get => _isCoop; }


    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager не существует!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isGameOver)
        {
            if (_isCoop)
            {
                SceneManager.LoadScene(2); // 2 — это индекс кооперативной сцены в игре
            }
            else
            {
                SceneManager.LoadScene(1); // 1 — это индекс основной сцены в игре
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // 0 — это индекс меню в игре
        }


    }

    public void Score(int scoreInc)
    {
        _score += scoreInc;
        Debug.Log("+" + scoreInc + " очков!");
        _uiManager.UpdateScore(_score);
    }
}
