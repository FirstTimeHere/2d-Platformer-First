using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Transform _childTransform;

    public event Action<bool> Jumped, Ran, CameBackIdle;

    private Rigidbody2D _rigidbody;

    private bool _isGrounded;

    private Vector2 _negativeScale;
    private Vector2 _defaultScale;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _negativeScale = _childTransform.localScale;
        _defaultScale = _childTransform.localScale;
        _negativeScale.x *= -1;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(new Vector2(horizontal, 0) * _speed * Time.deltaTime);

        if (horizontal < 0)
        {
            _childTransform.localScale = _negativeScale;
        }
        else
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
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _isGrounded = true;
    }
}
