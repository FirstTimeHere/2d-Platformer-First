using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;
    private Enemy _enemy;

    private float _health;
    private float _delay = 1f;

    private bool _isTimeOut;

    private Coroutine _coroutine;

    public float Damage { get; private set; }

    public event Action Attacked;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetBorders(Border border, Border secondBorder)
    {
        _mover.GetBorders(border, secondBorder);
    }

    public void TakeDamageFireBall(FireBall fireBall)
    {
        _health -= fireBall.Damage;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void SetDamage(float damage)
    {
        Damage = damage;
    }

    public void SetSpeed(float speed)
    {
        _mover.GetSpeed(speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_coroutine == null)
            {
                _isTimeOut = true;
                _coroutine = StartCoroutine(GetAttackTime(_delay, _isTimeOut));
            }

            player.TakeDamage(_enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isTimeOut = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }

    private IEnumerator GetAttackTime(float delay, bool isTimeOut)
    {
        var wait = new WaitForSeconds(delay);

        while (isTimeOut)
        {
            Attacked?.Invoke();

            yield return wait;
        }
    }
}
