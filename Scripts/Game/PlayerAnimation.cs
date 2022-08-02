using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour

{

    private Animator _animator;
    private Player _player;
    private KeyCode _leftBut;
    private KeyCode _rightBut;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();

        if (_player.IsPlayerOne)
        {
            _leftBut = KeyCode.A;
            _rightBut = KeyCode.D;
        }
        else
        {
            _leftBut = KeyCode.LeftArrow;
            _rightBut = KeyCode.RightArrow;
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(_leftBut))
        {
            _animator.SetBool("isTurnLeft", true);
            _animator.SetBool("isTurnRight", false);
        }
        else if(Input.GetKeyUp(_leftBut))
        {
            _animator.SetBool("isTurnLeft", false);
            _animator.SetBool("isTurnRight", false);
        }

        if (Input.GetKeyDown(_rightBut))
        {
            _animator.SetBool("isTurnLeft", false);
            _animator.SetBool("isTurnRight", true);
        }
        else if (Input.GetKeyUp(_rightBut))
        {
            _animator.SetBool("isTurnLeft", false);
            _animator.SetBool("isTurnRight", false);
        }

    }
}
