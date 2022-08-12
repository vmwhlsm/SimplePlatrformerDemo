using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    private const string _moveRight = "d";
    private const string _moveLeft = "a";
    private const string _jump = "space";
    private const string JumpTrigger = "Jump";
    private const string RunBool = "Run";
    private bool _isMoveDirectionRight;
    private bool _isMoveDirectionLeft;
    private Rigidbody2D _rb2d;
    private float _speed = 0.25f;
    private float _jumpSpeed = 10f;
    private bool _grounded;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(ManageActions());
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        CountRacycastHits();
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        _grounded = false;
    }

    private void Update()
    {
        _isMoveDirectionLeft = Input.GetKey(_moveLeft);
        _isMoveDirectionRight = Input.GetKey(_moveRight);        
    }

    private void CountRacycastHits()
    {
        RaycastHit2D[] hits;

        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll(positionToCheck, Vector2.down, 0.01f);

        if (hits.Length > 0)
        {
            _grounded = true;
        }
    }

    private IEnumerator ManageActions()
    {
        while (true)
        {
            _animator.SetBool(RunBool, _isMoveDirectionLeft || _isMoveDirectionRight);

            if (_isMoveDirectionLeft)
            {
                _spriteRenderer.flipX = true;
                Move(Vector2.left);
            }

            if (_isMoveDirectionRight)
            {
                _spriteRenderer.flipX = false;
                Move(Vector2.right);
            }

            if (Input.GetKeyDown(_jump) && _grounded)
            {
                _animator.SetTrigger(JumpTrigger);
                _rb2d.velocity += _jumpSpeed * Vector2.up;
            }

            yield return null;
        }
    }

    private void Move(Vector2 target)
    {
        _rb2d.position = _rb2d.position + target * _speed;
    }
}
