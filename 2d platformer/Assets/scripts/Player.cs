using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _items;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private Transform _positionMask;
    [field: SerializeField] public Transform ChildTransform { get; private set; }

    private PlayerMover _playerMover;
    private Health _health;
    private Wallet _wallet;
    private KeyCodes _keys = new KeyCodes();

    private List<Weapon> _weapons = new List<Weapon>();

    public event Action Atacked;
    public event Action Hurted;
    public event Action AddedWeapon;

    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        _weapons.Add(_defaultWeapon);

        AddedWeapon?.Invoke();

        Collider = GetComponent<Collider2D>();
        _playerMover = GetComponent<PlayerMover>();
        _health = GetComponent<Health>();
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _health.Dead += OnDead;
    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
    }

    private void Update()
    {
        Attack();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Chest chest))
        {
            if (Input.GetKeyDown(_keys.Interection) && IsInterection())
            {
                chest.SetOpen();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            if (item is Coin coin)
            {
                _wallet.TakeMoney(coin);
                Destroy(coin.gameObject);
            }
            else if (item is MedKit medKit)
            {
                _health.TakeHeal(medKit.Value);
                Destroy(medKit.gameObject);
            }
        }

        if (collision.TryGetComponent(out Weapon weapon))
        {
            if (weapon is VampireMask mask)
            {
                mask.transform.position = _positionMask.position;
                _weapons.Add(mask);
                AddedWeapon?.Invoke();
            }
        }
    }

    private void OnDead()
    {
        gameObject.SetActive(false);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(_keys.Attack))
        {
            Atacked?.Invoke();
        }
    }

    private bool IsInterection()
    {
        Vector2 point = new Vector2(Collider.bounds.center.x, Collider.bounds.min.y);
        Vector2 size = new Vector2(Collider.bounds.size.x * 0.55f, 0.01f);

        return Physics2D.OverlapBox(point, size, 0, _items);
    }

    public Vector2 GetChildLocalScale()
    {
        return _playerMover.GetChildLocalScale();
    }

    public void TakeDamage(Enemy enemy)
    {
        Hurted?.Invoke();
        _health.TakeDamage(enemy.Damage);
    }

    public void TakeVampireHeal(float heal)
    {
        _health.TakeHeal(heal);
    }

    public List<Weapon> SetWeapons()
    {
        List<Weapon> tempWeapons = new List<Weapon>();

        foreach (Weapon weapon in _weapons)
        {
            tempWeapons.Add(weapon);
        }

        return tempWeapons;
    }
}
