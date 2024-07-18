using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _items;

    private Collider2D _playerCollider;

    private KeyCode _interaction = KeyCode.E;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
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
            if (Input.GetKey(_interaction) && CheckInterection())
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
