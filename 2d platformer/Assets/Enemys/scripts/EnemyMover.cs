using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Border _borderBegin, _borderEnd;

    private bool _isItBorderBegin;

    public Action<bool> GetJump, GetIdle, GetRun;

    private Vector2 _negativeScale;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _negativeScale = transform.localScale;
        _defaultScale = transform.localScale;
        _negativeScale.x *= -1;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_isItBorderBegin)
        {
            transform.position = Vector2.MoveTowards(transform.position, _borderEnd.transform.position, _speed * Time.deltaTime);
            transform.localScale=_negativeScale;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _borderBegin.transform.position, _speed * Time.deltaTime);
            transform.localScale=_defaultScale;
        }

        GetRun?.Invoke(true);
    }

    public void GetBorders(Border border, Border secondBorder)
    {
        _borderBegin = border;
        _borderEnd = secondBorder;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _borderBegin.SetCollider())
        {
            _isItBorderBegin = true;
        }
        else if (collision == _borderEnd.SetCollider())
        {
            _isItBorderBegin = false;
        }
    }
}
