using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8;
    [SerializeField]
    private float _destroyHeight = 8.0f;
    [SerializeField]
    private bool _isEnemy;
    [SerializeField]
    private AudioClip _expSound;

    public bool IsEnemy { get => _isEnemy; set => _isEnemy = value; }

    void Update()
    {
        if (transform.position.y >= _destroyHeight && !IsEnemy)
        {

            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
        else if (transform.position.y <= _destroyHeight && IsEnemy)
        {
            Destroy(gameObject);
        }
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        Vector3 direction = new Vector3(0, _speed, 0);
        transform.Translate(direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && IsEnemy)
        {
            Player player = collision.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
                AudioSource.PlayClipAtPoint(_expSound, transform.position);
                Destroy(transform.gameObject);
            }

        }
    }


}
