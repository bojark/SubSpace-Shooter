using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private WorldLogic _worldLogic;
    private float _borderY = 0;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _workingTime = 5.0f;
    [SerializeField]
    private int _powerupID; //0 — щит, 1 — скорость, 2 — триплшот, 3 — иридиум, 4 — лечилка
    [SerializeField]
    private AudioClip _powerUpSound;


    private void Start()
    {
        _worldLogic = GameObject.Find("World Logic").GetComponent<WorldLogic>();

        if (_worldLogic == null)
        {
            Debug.LogError("World Logic is NULL!");
        }
        else
        {
            _borderY = _worldLogic.BorderY;
        }
    }

    void Update()
    {

        if (transform.position.y <= -_borderY)
        {
            Debug.Log(transform.name + " упущен.");
            Destroy(gameObject);
        }
        else
        {
            Movement();
        }

    }

    private void Movement()
    {
        transform.Translate(Vector3.down *_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(transform.name + " столкнулся с " + other.name);

        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                Debug.Log(transform.name + " подобран.");
                AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
                
                switch(_powerupID)
                {
                    case 0:
                        Debug.Log("Ты подобрал щит!");
                        player.PowerupShield();
                        _worldLogic.Score(5);
                        break;
                    case 1:
                        Debug.Log("Ты подобрал спидуху!");
                        player.PowerupSpeed(_workingTime, 3);
                        _worldLogic.Score(5);
                        break;
                    case 2:
                        Debug.Log("Ты подобрал триплшот!");
                        player.PowerupTripleShot(_workingTime);
                        _worldLogic.Score(5);
                        break;
                    case 3:
                        Debug.Log("Ты подобрал иридиум!");
                        _worldLogic.Score(30);
                        break;
                    case 4:
                        Debug.Log("Ты подобрал ремонтный набор!");
                        player.Repair();
                        break;
                    default:
                        Debug.Log("Ты подобрал что-то (хз что)!");
                        break;
                }

                Destroy(gameObject);

            }
            else
            {
                Debug.LogError("Игрока не существует.");
            }

        }
    }

}
