using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InvisibleWall : MonoBehaviour
{
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

    private BoxCollider2D _wall;

    private float _wallSizeX = 199.029999f;
    private float _wallSizeY = 1f;

    private void Awake()
    {
        _wall = GetComponent<BoxCollider2D>();
        _wall.isTrigger = true;
        _wall.size = new Vector2(_wallSizeX, _wallSizeY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.transform.position = _playerSpawnPoint.transform.position;
        }
    }
}
