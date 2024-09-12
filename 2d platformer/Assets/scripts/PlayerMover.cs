using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private Transform _childTransform;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private Player _player;
    private KeyCodes _key = new KeyCodes();

    private bool _isGrounded;

    private Vector2 _negativeScale;
    private Vector2 _defaultScale;

    public event Action<bool> Jumped;
    public event Action<bool> Ran;
    public event Action<bool> CameBackIdle;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = _player.Collider;

        _negativeScale = _childTransform.localScale;
        _defaultScale = _childTransform.localScale;
        _negativeScale.x *= -1;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGround();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(Horizontal);

        transform.Translate(new Vector2(horizontal, 0) * _speed * Time.deltaTime);

        if (horizontal < 0)
        {
            _childTransform.localScale = _negativeScale;
        }
        else if (horizontal > 0)
        {
            _childTransform.localScale = _defaultScale;
        }

        Ran?.Invoke(true);

        if (horizontal == 0)
        {
            Ran?.Invoke(false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(_key.Jump) && _isGrounded)
        {
            Jumped?.Invoke(true);
            Ran?.Invoke(false);
            CameBackIdle?.Invoke(false);

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            Jumped?.Invoke(false);
            CameBackIdle?.Invoke(true);
        }
    }

    private bool CheckGround()
    {
        Vector2 point = new Vector2(_collider.bounds.center.x, _collider.bounds.min.y);
        Vector2 size = new Vector2(_collider.bounds.size.x * 0.55f, 0.01f);

        return Physics2D.OverlapBox(point, size, 0, _groundMask);
    }

    public Vector2 GetChildLocalScale()
    {
        return _childTransform.localScale;
    }
}
