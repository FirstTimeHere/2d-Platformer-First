using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;

    private float _health;
    private float _delay = 1f;

    private bool _isTimeOut;

    private Coroutine _coroutine;

    private Enemy _enemy;

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
        Player player = collision.GetComponent<Player>();

        if (player)
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
        if (collision.GetComponent<Player>())
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
