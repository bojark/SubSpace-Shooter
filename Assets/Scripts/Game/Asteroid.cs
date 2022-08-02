using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _speedRotation = 15.0f;
    private float _speed = 1f;
    private WorldLogic _worldLogic;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    private bool _isActive;

    private void Start()
    {
        
        _worldLogic = GameObject.Find("World Logic").GetComponent<WorldLogic>();
        if (_worldLogic == null)
        {
            Debug.LogError("World Logic is NULL");
        }
        else
        {
            _speed = _worldLogic.AsteroidSpeed;
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is NULL");
        }

        _isActive = true;

    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Астероид с чем-то взаимодействует");
        if (other.CompareTag("Laser") && _isActive)
        {
            Destroy(other.gameObject);
            _isActive = false;
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _spawnManager.Spawn();
            Destroy(gameObject, 1f);
        }
    }
}
