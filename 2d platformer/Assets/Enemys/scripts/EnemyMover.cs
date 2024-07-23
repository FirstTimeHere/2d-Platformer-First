using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMover : MonoBehaviour
{
    private Border _borderBegin, _borderEnd;

    private bool _isItBorderBegin;

    private float _speed;

    private Vector2 _negativeScale;
    private Vector2 _defaultScale;

    public event Action<bool> Jumped, CameBackIdle, Ran;

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
        if (SeePlayer(out Transform target))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }
        else
        {
            if (_isItBorderBegin)
            {
                transform.position = Vector2.MoveTowards(transform.position, _borderEnd.transform.position, _speed * Time.deltaTime);
                transform.localScale = _negativeScale;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, _borderBegin.transform.position, _speed * Time.deltaTime);
                transform.localScale = _defaultScale;
            }
        }

        Ran?.Invoke(true);
    }

    private bool SeePlayer(out Transform target)
    {
        Vector3 direction = _borderBegin.transform.position - _borderEnd.transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

        foreach (RaycastHit2D hit2D in hits)
        {
            if (hit2D.rigidbody.gameObject.GetComponent<Player>())
            {
                target = hit2D.collider.gameObject.transform;
                Debug.Log(target);
                return true;
            }
        }

        target = null;

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _borderBegin.GetCollider())
        {
            _isItBorderBegin = true;
        }
        else if (collision == _borderEnd.GetCollider())
        {
            _isItBorderBegin = false;
        }
    }

    public void GetSpeed(float speed)
    {
        _speed = speed;
    }

    public void GetBorders(Border border, Border secondBorder)
    {
        _borderBegin = border;
        _borderEnd = secondBorder;
    }
}
