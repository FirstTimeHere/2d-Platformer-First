using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireBall))]
public class FireBallMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _direction = new Vector2(1, 0);
    private Vector2 _directionBack = new Vector2(-1, 0);
    private Vector2 _playerTransform;
    private Vector2 _localScale;
    private Vector2 _NegativeLocalScale;

    private void Awake()
    {
        _localScale = transform.localScale;
        _NegativeLocalScale = transform.localScale;
        _NegativeLocalScale.x *= -1;
    }

    private void Update()
    {
        Move();
    }

    public void GetPlayerLocalScale(Vector2 playerLocalScale)
    {
        _playerTransform = playerLocalScale;
    }

    private void Move()
    {
        if (_playerTransform.x < 0)
        {
            transform.Translate(_directionBack * Time.deltaTime * _speed);
            transform.localScale = _NegativeLocalScale;
        }
        else
        {
            transform.Translate(_direction * Time.deltaTime * _speed);
            transform.localScale = _localScale;
        }
    }
}
