using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private WorldLogic _worldLogic;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _laser3Prefab;
    [SerializeField]
    private float _speedX = 3.5f;
    [SerializeField]
    private float _speedY = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 1.0f;
    [SerializeField]
    private float _borderX;
    [SerializeField]
    private float _borderY;
    [SerializeField]
    private float _shotY = 1.05f;
    [SerializeField]
    private float _canFire = -1;
    [SerializeField]
    private float _coolDownTime = 0.5f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _is3ShotActive = false;
    private float _puWorkingTime;
    [SerializeField]
    private bool _isShieldOn = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    //private int _score = 0;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject[] _engineDamage = new GameObject[2];
    [SerializeField]
    private AudioSource _laserSound;
    [SerializeField]
    private AudioSource _backGroundMusic;
    [SerializeField]
    private GameObject _speedVisualEffect;
    [SerializeField]
    private int _playerNum = 1;

    public bool Is3ShotActive { get => _is3ShotActive; set => _is3ShotActive = value; }
    public float PuWorkingTime { get => _puWorkingTime; set => _puWorkingTime = value; }

    void Start()
    {
        if (_worldLogic.IsCoop)
        {
            if(_playerNum == 1)
            {
                transform.position = new Vector2(-5, 0);
            }
            else
            {
                transform.position = new Vector2(5, 0);
            }
            
        }
        else
        {
            transform.position = new Vector2(0, 0);
        }
        
        

        _borderX = _worldLogic.PlayerBorderX;
        _borderY = _worldLogic.PlayerBorderY;
        
        _speedVisualEffect.SetActive(false);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager == null)
        {
            Debug.LogError("UI Manager не существует!");
        }

        for(int i=0; i <_engineDamage.Length; i++)
            {
            _engineDamage[i].SetActive(false);
            }

    }

    void Update()
    {
        Movement();

        //Стрельба из лазера

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            LaserShot();
        }

        //отображение щита

        if (_isShieldOn)
        {
            _shield.SetActive(true);
        }
        else
        {
            _shield.SetActive(false);
        }

    }

    void Movement() 
    {
        float horInput = Input.GetAxis("Horizontal");   //берём силу нажатия кнопки из инпута "Horizontal"
        float verInput = Input.GetAxis("Vertical");     //берём силу нажатия кнопки из инпута "Vertical"
        Vector3 direction = new Vector2(horInput * _speedX * _speedMultiplier, verInput * _speedY * _speedMultiplier);

        transform.Translate(direction * Time.deltaTime);

        //Ограничиваем передвижение Player

        if (transform.position.x >= _borderX)
        {
            transform.position = new Vector2(-_borderX, transform.position.y);
        }
        else if (transform.position.x <= -_borderX)
        {
            transform.position = new Vector2(_borderX, transform.position.y);
        }

        //передвижение по Y можно ограничить с помощью специального метода Mathf.Clamp()

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _borderY, 0), 0);

    }

    void LaserShot()
    {
        _canFire = Time.time + _coolDownTime;

        if (Is3ShotActive)
        {
            _laserSound.Play();
            Instantiate(_laser3Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
        else
        {
            _laserSound.Play();
            Instantiate(_laserPrefab, new Vector2(transform.position.x, transform.position.y + _shotY), Quaternion.identity);
        }

    }

    public void Damage()
    {

        if (_isShieldOn)
        {
            Debug.Log("Щит уничтожен!");
            _isShieldOn = false;
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);
        LiveCheck();
    }

    IEnumerator EngineCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        _engineDamage[Random.Range(0, 2)].SetActive(true);
        
    }

    IEnumerator EngineCoroutine2()
    {
        yield return new WaitForSeconds(0.2f);
        _engineDamage[0].SetActive(true);
        _engineDamage[1].SetActive(true);
        
    }

    public void PowerupTripleShot(float workingTime)
    {
        _is3ShotActive = true;
        _puWorkingTime = workingTime;
        Debug.Log("Паверап 'тройной лазер' активен на " + (int)workingTime + " секунд.");
        StartCoroutine("TripleShotCoroutine");

    }
    IEnumerator TripleShotCoroutine()
    {
        Debug.Log("Осталось секунд: " + (int)_puWorkingTime);
        float end = Time.time + _puWorkingTime;

        while (Time.time <= end)
        {
            Debug.Log("Триплшот. Осталось секунд: " + (int)(end - Time.time));
            _is3ShotActive = true;
            yield return new WaitForSeconds(1);
        }

        Debug.Log("У тебя снова один лазер.");
        _is3ShotActive = false;
    }

    public void PowerupSpeed (float workingTime, float multiplier)
    {
        _speedMultiplier = multiplier;
        _puWorkingTime = workingTime;
        Debug.Log("Скорость игрока увеличена на " + _speedMultiplier);
        StartCoroutine(SpeedupCoroutine(multiplier));
    }

    IEnumerator SpeedupCoroutine(float multiplier)
    {
        float end = Time.time + _puWorkingTime;
        _backGroundMusic.pitch = 1.2f;
        _speedVisualEffect.SetActive(true);
        while (Time.time <= end)
        {
            Debug.Log("Ускорение. Осталось секунд: " + (int)(end - Time.time));
            _speedMultiplier = multiplier;
            yield return new WaitForSeconds(1);
        }

        _speedMultiplier = 1;
        _backGroundMusic.pitch = 0.6f;
        _speedVisualEffect.SetActive(false);
        Debug.Log("Скорость игрока вернулась к исходному значению.");
    }

    public void PowerupShield()
    {
        _isShieldOn = true;
        Debug.Log("Щит активен.");
    }

    /*public void Score(int scoreInc)
    {
        _score += scoreInc;
        Debug.Log("+" + scoreInc + " очков!");
        _uiManager.UpdateScore(_score);
    }
    */

    public void Repair()
    {
        if(_lives < 3 && _worldLogic.IsActive)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
            LiveCheck();
        }

    }

    public void LiveCheck()
    {
        switch (_lives)
        {
            case 3:
                _engineDamage[0].SetActive(false);
                _engineDamage[1].SetActive(false);
                break;
            case 2:
                _engineDamage[0].SetActive(false);
                _engineDamage[1].SetActive(false);
                StartCoroutine(EngineCoroutine());
                break;
            case 1:
                StartCoroutine(EngineCoroutine2());
                break;
            case 0:
                Debug.Log("Player уничтожен, игра окончена");
                _worldLogic.IsActive = false;
                Destroy(gameObject);
                break;
        }
    }
}   
