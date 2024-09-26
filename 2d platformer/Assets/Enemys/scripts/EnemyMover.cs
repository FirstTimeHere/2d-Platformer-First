using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    [SerializeField] private Transform _healthBar;

    private Border _borderBegin, _borderEnd;

    private bool _isItBorderBegin;

    private float _speed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _borderBegin.Collider)
        {
            _isItBorderBegin = true;
        }
        else if (collision == _borderEnd.Collider)
        {
            _isItBorderBegin = false;
        }
    }

    private void Move()
    {
        if (TryDectedPlayer())
        { 
            Transform target = TryDectedPlayer();

            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }

        if (_isItBorderBegin)
        {
            transform.position = Vector2.MoveTowards(transform.position, _borderEnd.transform.position, _speed * Time.deltaTime);
            transform.rotation = _negativeScale;
            _healthBar.localRotation = _negativeScale;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _borderBegin.transform.position, _speed * Time.deltaTime);
            transform.rotation = _defaultScale;
            _healthBar.localRotation = _defaultScale;
        }

        Ran?.Invoke(true);
    }

    private Transform TryDectedPlayer()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -transform.right, _distance, _targetMask);

        if (hit2D)
        {
            if (hit2D.collider.TryGetComponent(out Player player))
            {
                return hit2D.collider.transform;
            }
        }

        return null;
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
