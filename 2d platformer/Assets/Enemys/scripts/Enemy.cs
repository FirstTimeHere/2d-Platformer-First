using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;
    private Enemy _enemy;
    private Health _health;

    private float _delay = 1f;

    private bool _isTimeOut;

    private Coroutine _coroutine;

    public event Action Attacked;

    public float Damage { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _enemy = GetComponent<Enemy>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Dead += OnDead;
    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
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
    private void OnDead()
    {
        Destroy(gameObject);
    }

    public void SetBorders(Border border, Border secondBorder)
    {
        _mover.GetBorders(border, secondBorder);
    }

    public void TakeDamage(Ammunition ammunition)
    {
        _health.TakeDamage(ammunition.Damage);
    }

    public void TakeDamage(Weapon weapon)
    {
        _health.TakeDamage(weapon.Value);
    }

    public void SetDamage(float damage)
    {
        Damage = damage;
    }

    public void SetSpeed(float speed)
    {
        _mover.GetSpeed(speed);
    }
}
