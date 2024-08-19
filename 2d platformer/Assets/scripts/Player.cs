using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _items;

    private Collider2D _playerCollider;

    private PlayerMover _playerMover;
    private Player _player;

    private KeyCode _interaction = KeyCode.E;
    private KeyCode _attack = KeyCode.Mouse0;

    private float _maxHealth;
    private float _money = 0;

    public float Health { get; private set; } = 10000f;

    public event Action Atacked, Hurted;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
        _playerMover = GetComponent<PlayerMover>();
        _maxHealth = Health;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        Attack();
    }

    public Collider2D GetCollider()
    {
        return _playerCollider;
    }

    private bool CheckInterection()
    {
        Vector2 point = new Vector2(_playerCollider.bounds.center.x, _playerCollider.bounds.min.y);
        Vector2 size = new Vector2(_playerCollider.bounds.size.x * 0.55f, 0.01f);

        return Physics2D.OverlapBox(point, size, 0, _items);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Chest>())
        {
            Chest chest = collision.gameObject.GetComponent<Chest>();

            if (Input.GetKey(_interaction) && CheckInterection())
            {
                chest.GetOpen();
                chest.DisableCollider(chest);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>())
        {
            Item item = collision.gameObject.GetComponent<Item>();
            item.GetAbility(_player);
            Destroy(item.gameObject);
        }
    }

    public Vector2 GetChildLocalScale()
    {
        return _playerMover.GetChildLocalScale();
    }

    public void TakeDamage(Enemy enemy)
    {
        Health -= enemy.Damage;
        Hurted?.Invoke();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(_attack))
        {
            Atacked?.Invoke();
        }
    }

    public void TakeHeal(float heal)
    {
        Health += heal;

        if (Health > _maxHealth)
            Health = _maxHealth;
    }

    public void TakeMoney(float money)
    {
        _money += money;
    }
}
