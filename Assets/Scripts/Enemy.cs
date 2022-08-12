using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint1;
    [SerializeField] private Transform _targetPoint2;

    private Rigidbody2D _rb2d;
    private float _reachDistance = 0.05f;
    private SpriteRenderer _spriteRenderer;
    private float _speed = 0.25f;
    private Vector3 _activePoint;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.flipX = true;
        _activePoint = _targetPoint1.position;
    }

    private void Update()
    {
        if(Mathf.Abs(transform.position.x - _targetPoint1.position.x) <= _reachDistance)
        {
            _activePoint = _targetPoint2.position;
            _spriteRenderer.flipX = false;
        }

        if(Mathf.Abs(transform.position.x - _targetPoint2.position.x) <= _reachDistance)
        {
            _activePoint = _targetPoint1.position;
            _spriteRenderer.flipX = true;
        } 

        Move(_activePoint);
    }

    private void Move(Vector2 target)
    {
        transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, target.x, _speed), transform.position.y);
    }
}
