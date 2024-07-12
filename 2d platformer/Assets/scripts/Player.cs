using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private Collider2D _playerCollider;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
    }

    public Collider2D SetCollider()
    {
        return _playerCollider;
    }
}