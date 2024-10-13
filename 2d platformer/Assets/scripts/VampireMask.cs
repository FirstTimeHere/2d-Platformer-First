using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class VampireMask : Weapon
{
    [SerializeField] private float _value;
    [SerializeField] private float _direction;
    [SerializeField] private float _precentHeal;
    [SerializeField] private float _time = 5f;

    [SerializeField] private LayerMask _targetMask;

    private float _heal;
    private float _delay = 1f;
    private float _tempTime;

    private bool _isAttack;

    private KeyCodes _keys = new KeyCodes();

    private Coroutine _coroutine;

    private Vector2 _localScale;
    private Vector2 _rotateByScale;

    private Player _player;
    private VampireMask _mask;

    private Collider2D _collider;

    private WaitForSeconds _wait;

    public override float Value => _value;

    public override float ReloadTime => _tempTime;

    public float Heal { get; private set; }

    private void Awake()
    {
        _mask = GetComponent<VampireMask>();
        _collider = GetComponent<Collider2D>();

        _localScale = transform.localScale;
        _rotateByScale = transform.localScale;

        _wait = new WaitForSeconds(_delay);

        _heal = _value / _precentHeal;
        Heal = _heal;
        _tempTime = _time;
    }

    private void FixedUpdate()
    {
        transform.localScale = _rotateByScale;
    }

    private void Update()
    {
        if (_player != null)
        {
            bool isUserClicked = Input.GetKey(_keys.Attack);
            _isAttack = isUserClicked;

            if (isUserClicked)
            {
                Enemy enemy;

                if (IsItEnemy(_player.ChildTransform.localScale) && _tempTime > 0)
                {
                    _tempTime--;
                    enemy = IsItEnemy(_player.ChildTransform.localScale);
                    Stop();
                    _coroutine = StartCoroutine(GetUseAblity(enemy));
                }
            }
            else if (_tempTime <= 0)
            {
                Stop();
                _coroutine = StartCoroutine(ReturnTimerBack());
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
            Heal = enemyHealth/_precentHeal;
            return Heal;
        }

        return _heal;
    }

    private Enemy IsItEnemy(Vector3 rotationPlayer)
    {
        RaycastHit2D hit;

        if (rotationPlayer.x < 0)
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, _direction, _targetMask);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, transform.right, _direction, _targetMask);
        }

        if (hit)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                return enemy;
            }
        }

        return null;
    }

    private IEnumerator GetUseAblity(Enemy enemy)
    {
        while (_isAttack && _tempTime > 0)
        {
            enemy.TakeDamage(_mask);
            _player.TakeVampireHeal(GetHeal(enemy));

            yield return _wait;
        }
    }

    private IEnumerator ReturnTimerBack()
    {
        while (_tempTime < _time)
        {
            _tempTime++;

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