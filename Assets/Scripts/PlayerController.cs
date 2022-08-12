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
    private Vector2 _moveVector;
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
    }

    private void Update()
    {
        _animator.SetBool(RunBool, _moveVector == Vector2.left || _moveVector == Vector2.right);
        ManageKey();
        Move(_moveVector);
        Jump();
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        _grounded = IsGrounded();
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        _grounded = false;
    }

    private void ManageKey()
    {
        if (Input.GetKeyDown(_moveLeft))
        {
            _spriteRenderer.flipX = true;
            _moveVector = Vector2.left;
        }

        if (Input.GetKeyDown(_moveRight))
        {
            _spriteRenderer.flipX = false;
            _moveVector = Vector2.right;
        }

        if (Input.GetKeyUp(_moveLeft) || Input.GetKeyUp(_moveRight))
            _moveVector = Vector2.zero;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(_jump) && _grounded)
        {
            _animator.SetTrigger(JumpTrigger);
            _rb2d.velocity += _jumpSpeed * Vector2.up;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D[] hits;

        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll(positionToCheck, Vector2.down, 0.01f);

        if (hits.Length > 0)
        {
            return true;            
        }
        else
        {
            return false;
        }
    }

    private void Move(Vector2 target)
    {
        _rb2d.position = _rb2d.position + target * _speed;
    }
}
