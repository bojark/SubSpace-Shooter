using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField]
    private float _spaceSpawnY = -10.85f;
    [SerializeField]
    private float _spaceDestroyY = -20.91f;
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private GameObject _spacePrefab;
    private bool _isSpawned = false;
    private WorldLogic _worldLogic;
    private int _spaceNum = 1;
    private Player _player;

    void Start()
    {

        transform.Translate(new Vector3(0, 0, 0));
        _worldLogic = GameObject.Find("World Logic").GetComponent<WorldLogic>();

        if (_worldLogic == null)
        {
            Debug.LogError("World Logic is NULL!");
        }
        else
        {
            _speed = _worldLogic.SpaceSpeed;
            _spaceNum = _worldLogic.SpaceNum;
            _player = _worldLogic.Player;

        }

    }

    void Update()
    {

        if(transform.position.y <= _spaceSpawnY && !_isSpawned && _worldLogic.IsActive)
        {
            Vector3 spawnPos = new Vector3(0, 20.7f, 0);
            GameObject newSpace = Instantiate(_spacePrefab, spawnPos, Quaternion.identity);
            newSpace.transform.name = "Space "+ _spaceNum;
            _isSpawned = true;
            _worldLogic.SpaceNum++;
        }
        else if(transform.position.y <= _spaceDestroyY)
        {
            Debug.Log("Космос отжил своё!");
            if (!_worldLogic.IsCoop)
            {
                _worldLogic.Score(_player.IsPlayerOne, 5);
            }
            Destroy(gameObject);
        }

        if(_worldLogic.IsActive)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        } 

    }


}
