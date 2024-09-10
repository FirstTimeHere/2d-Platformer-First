using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Transform _spriteHealthBar;

    private Border _borderBegin, _borderEnd;

    private bool _isItBorderBegin;

    private float _speed;
    private float _speedForPlayer;
    private float _distance;

    private Quaternion _negativeScale;
    private Quaternion _defaultScale;

    public event Action<bool> Ran;

    private void Awake()
    {
        _negativeScale = Quaternion.Euler(0, 180f, 0);
        _defaultScale = Quaternion.identity;
    }

    private void Start()
    {
        _distance = _borderEnd.transform.position.x - _borderBegin.transform.position.x;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (TryDectedPlayer())
        {
            Transform target = TryDectedPlayer();

            transform.position = Vector2.MoveTowards(transform.position, target.position, _speedForPlayer * Time.deltaTime);
        }

        if (_isItBorderBegin)
        {
            transform.position = Vector2.MoveTowards(transform.position, _borderEnd.transform.position, _speed * Time.deltaTime);
            _spriteHealthBar.localRotation = _negativeScale;
            transform.rotation = _negativeScale;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _borderBegin.transform.position, _speed * Time.deltaTime);
            _spriteHealthBar.localRotation = _defaultScale;
            transform.rotation = _defaultScale;
        }

        Ran?.Invoke(true);
    }

    private Transform TryDectedPlayer()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -transform.right, _distance, _targetMask);

        if (hit2D)
        {
            if (hit2D.collider.GetComponent<Player>())
            {
                return hit2D.collider.transform;
            }
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _borderBegin.ColliderBorder)
        {
            _isItBorderBegin = true;
        }
        else if (collision == _borderEnd.ColliderBorder)
        {
            _isItBorderBegin = false;
        }
    }

    public void GetSpeed(float speed, float speedForPlayer)
    {
        _speed = speed;
        _speedForPlayer = speedForPlayer;
    }

    public void GetBorders(Border border, Border secondBorder)
    {
        _borderBegin = border;
        _borderEnd = secondBorder;
    }
}
