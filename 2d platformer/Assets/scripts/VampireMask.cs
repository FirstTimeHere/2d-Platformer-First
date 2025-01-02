using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class VampireMask : Weapon
{
    [SerializeField] private float _value;
    [SerializeField] private float _direction;
    [SerializeField] private float _precentHeal;
    [SerializeField] private float _time;

    [SerializeField] private LayerMask _targetMask;

    private float _heal;
    private float _delay = 1f;
    private float _reloadTime;
    private float _radius = 2.5f;

    private Coroutine _coroutine;

    private Vector2 _localScale;
    private Vector2 _rotateByScale;
    private Vector2 _directionByVector;

    private Player _player;
    private VampireMask _mask;

    private Collider2D _collider;

    private WaitForSeconds _wait;

    public override float Value => _value;

    public override float ReloadTime => _reloadTime;

    public float Heal { get; private set; }

    public Action GetUse;
    public Action Reloaded;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();

        _directionByVector=new Vector2(_direction,0);

        _mask = GetComponent<VampireMask>();
        _collider = GetComponent<Collider2D>();

        _localScale = transform.localScale;
        _rotateByScale = transform.localScale;

        _wait = new WaitForSeconds(_delay);

        _heal = _value / _precentHeal;
        Heal = _heal;
        _reloadTime = _time;
    }

    private void OnEnable()
    {
        _player.Atacked += Attack;
    }

    private void OnDisable()
    {
        _player.Atacked -= Attack;
    }

    private void FixedUpdate()
    {
        transform.localScale = _rotateByScale;
    }

    private void Update()
    {
        if (_reloadTime==0)
        {
            Stop();
            _coroutine = StartCoroutine(ReturnTimerBack());
        }
    }

    private void Attack()
    {
        if (_player != null)
        {
            if (_reloadTime > 0 || _reloadTime == _time)
            {
                Stop();
                _coroutine = StartCoroutine(GetUseAblity());
                GetUse?.Invoke();
            }
            else if (_reloadTime <= 0)
            {
                Stop();
                _coroutine = StartCoroutine(ReturnTimerBack());
                Reloaded?.Invoke();
            }
        }
    }

    private void Stop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private float GetHeal(Enemy enemy)
    {
        float enemyHealth = enemy.GetHealth();

        if (enemyHealth <= Value)
        {
            Heal = enemyHealth / _precentHeal;
            return Heal;
        }

        return _heal;
    }

    private Enemy IsItEnemy()
    {
        RaycastHit2D hit;

        hit = Physics2D.CircleCast(transform.position, _radius, _directionByVector, 0, _targetMask);

        if (hit)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                return enemy;
            }
        }

        return null;
    }

    private IEnumerator GetUseAblity()
    {
        Enemy enemy;

        while (_reloadTime > 0)
        {

            if (IsItEnemy())
            {
                enemy = IsItEnemy();
                enemy.TakeDamage(_mask);
                _player.TakeVampireHeal(GetHeal(enemy));

                _reloadTime--;
            }

            yield return _wait;
        }
    }

    private IEnumerator ReturnTimerBack()
    {
        while (_reloadTime < _time)
        {
            _reloadTime++;

            yield return _wait;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _collider.enabled = false;
            _player = player;
            transform.parent = _player.ChildTransform;
            transform.localScale = _localScale;
            _rotateByScale = _player.ChildTransform.localScale;
            gameObject.SetActive(false);
        }
    }
}