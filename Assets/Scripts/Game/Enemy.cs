using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private WorldLogic _worldLogic;
    private float _borderX;
    private float _borderY;
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;
    [SerializeField]
    private GameObject _laserPrefab;
    private bool _isDestroyed = false;
    private AudioSource _explosionSound;
    [SerializeField]
    private AudioClip _laserSound;
    private int _shootingRate;

    public bool IsDestroyed { get => _isDestroyed; set => _isDestroyed = value; }

    void Start()
    {
        _worldLogic = GameObject.Find("World Logic").GetComponent<WorldLogic>();
       
        if (_worldLogic == null)
        {
            Debug.LogError("World Logic is NULL!");
        }

        _player = _worldLogic.Player;
        _borderX = _worldLogic.BorderX;
        _borderY = _worldLogic.BorderY;
        _animator = GetComponent<Animator>();

        if(_animator == null)
        {
            Debug.LogError("Hey, Animator is Null!");
        }

        _explosionSound = GetComponent<AudioSource>();
        _shootingRate = Random.Range(1,11);
        Debug.Log("Враг стреляет раз в " + _shootingRate + " секунд.");

        StartCoroutine(ShotCoroutine());

    }

    void Update()
    {
        Movement();

        if(transform.position.y < - _borderY && !IsDestroyed)
        {
            ReCreate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(transform.name + " столкнулся с: " + other.name);

        switch(other.tag)
        {
            case "Player":
                if (other != null && !IsDestroyed)
                {
                    _player = other.GetComponent<Player>();
                    _explosionSound.Play();
                    _player.Damage();
                    _worldLogic.Score(_player.IsPlayerOne, 5);
                }
                DestroyEnemy();
                break;
            case "Laser":
                if (!IsDestroyed)
                {
                    Destroy(other.gameObject);
                }
                if (!IsDestroyed)
                {
                    _explosionSound.Play();
                    _worldLogic.Score(_player.IsPlayerOne, 15);
                    DestroyEnemy();
                }
                break;
            case "Enemy":
                ReCreate();
                break;
            default:
                Debug.Log("Ну столкнулся и столкнулся.");
                break;
        }

    }

    void DestroyEnemy()
    {
        _animator.SetTrigger("OnEnemyDeath");
        IsDestroyed = true;
        Destroy(gameObject, 2.3f);
    }

    void Movement()
    {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void ReCreate ()
    {
        float posX = Random.Range(_borderX, - _borderX);
        transform.position = new Vector3(posX, _borderY, 0);
    }

    IEnumerator ShotCoroutine()
    {
        while (!_isDestroyed)
        {

            yield return new WaitForSeconds(_shootingRate);
            if(!_isDestroyed)
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
                AudioSource.PlayClipAtPoint(_laserSound, transform.position);
            }

        }

    }


}