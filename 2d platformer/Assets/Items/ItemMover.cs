using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Collider2D _collider;

    private Vector2 _leftJump = new Vector2(-1, 1);
    private Vector2 _rightJump = new Vector2(1, 1);
    private Vector2 _direction;

    private List<Vector2> _directionJump = new List<Vector2>();

    private void Awake()
    {
        AddDirection();

        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        _direction = _directionJump[Random.Range(0, _directionJump.Count)];
    }

    private void FixedUpdate()
    {
        if (transform.localPosition == (Vector3)_direction)
        {
            _collider.enabled = true;
        }
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, _direction, Time.deltaTime * _speed);
        }
    }

    private void AddDirection()
    {
        _directionJump.Add(_leftJump);
        _directionJump.Add(_rightJump);
    }
}
