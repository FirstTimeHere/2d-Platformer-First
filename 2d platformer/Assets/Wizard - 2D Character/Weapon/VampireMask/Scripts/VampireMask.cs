using System;
using System.Collections;
using UnityEngine;

public class VampireMask : Weapon
{
    [SerializeField] private float _value;
    [SerializeField] private float _direction;
    [SerializeField] private float _precentHeal;
    [SerializeField] private float _time = 5f;

    [SerializeField] private LayerMask _targetMask;

    private float _heal;
    private float _tempTime;
    private float _delay = 0.2f;

    private bool _isReady = true;

    private KeyCodes _keys = new KeyCodes();

    private Coroutine _coroutine;

    private Vector2 _directionVampire;

    private Player _player;
    private VampireMask _mask;

    private Collider2D _collider;
    /// <summary>
    /// делаем луч на определенное (короткое) расстояние и проверяем если есть враг запускает отсчет по времмени
    /// например секунд 5
    /// также сделать выбор между оружиями, если реализован подбор
    /// делать неактивными оружие, есди его заменили (потом возвращать)
    /// </summary>

    public override float Value => _value;

    public float Heal { get; private set; }

    private void Awake()
    {
        _mask = GetComponent<VampireMask>();
        _collider = GetComponent<Collider2D>();
        _directionVampire = new Vector2(_direction, 0);
        _heal = _value / _precentHeal;
        Heal = _heal;
        _tempTime = 5f;
    }

    private void Update()
    {
        if (_player != null)
        {
            bool isUserClicked = Input.GetKey(_keys.Attack);

            if (enabled)
            {
                Debug.DrawRay(transform.position, _directionVampire, Color.red);
                GetUse(isUserClicked);
            }
        }
    }

    private void GetUse(bool isUserClicked)
    {
        Enemy enemy = IsItEnemy();

        if (_tempTime == 0)
        {
            _isReady = false;
            Stop();
        }

        if (isUserClicked && IsItEnemy() && _isReady)
        {
            _coroutine = StartCoroutine(GetUseAblity(_delay, enemy));
        }
    }

    private void RealoadingWeapon()
    {
        _tempTime = _time;
        _isReady = true;
    }

    private void Stop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private Enemy IsItEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _direction, _targetMask);

        if (hit)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                return enemy;
            }
        }

        return null;
    }

    private IEnumerator GetUseAblity(float delay, Enemy enemy)
    {
        var wait = new WaitForSecondsRealtime(delay);

        while (_tempTime > 0)
        {
            enemy.TakeDamage(gameObject.GetComponent<Weapon>());
            _player.VampireHeal(_mask);
            _tempTime--;
            yield return wait;
        }

        Invoke(nameof(RealoadingWeapon), _time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _collider.enabled = false;
            _player = player;
            transform.parent=_player.transform;
        }
    }
}