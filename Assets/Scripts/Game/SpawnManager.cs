using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour

{
    private bool _isActive;
    private float _borderX;
    private float _borderY;
    private float _enemySpawnRate;

    [SerializeField]
    private WorldLogic _worldLogic;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;

    void Start()
    {
        
        _isActive = _worldLogic.IsActive;
        _borderX = _worldLogic.BorderX;
        _borderY = _worldLogic.BorderY;
        _enemySpawnRate = _worldLogic.EnemySpawnRate;
    }

    void Update()
    {

        _isActive = _worldLogic.IsActive;

    }

    public void Spawn()
    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine("SpawnPowerUpARoutine");
    }

    IEnumerator SpawnEnemyRoutine()
    {
        Debug.Log("Спаун врагов работает");
        yield return new WaitForSeconds(2.0f);
        while (_isActive)
        {
            
            Vector3 spawnPos = new Vector3(Random.Range(_borderX, -_borderX), _borderY, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnRate);
        }
            
    }

    IEnumerator SpawnPowerUpARoutine()
    {
        Debug.Log("Спаун поверапа работает");
        while (_isActive)
        {
            yield return new WaitForSeconds(Random.Range(20.0f, 5.0f));
            if(_isActive)
            {
                Vector3 spawnPos = new Vector3(Random.Range(_borderX, -_borderX), _borderY, 0);
                Instantiate(_powerups[Random.Range(0, _powerups.Length)], spawnPos, Quaternion.identity);
            }
                 
        }

    }

}
