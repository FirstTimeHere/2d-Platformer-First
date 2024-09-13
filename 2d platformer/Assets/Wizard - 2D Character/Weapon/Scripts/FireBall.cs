using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(FireBallAnimation))]
[RequireComponent(typeof(FireBallMover))]
public class FireBall : MonoBehaviour
{
    private float _fireBallLifeTimer = 2f;
   
    private FireBallMover _mover;

    public float Damage { get; private set; } = 30f;

    private void Awake()
    {
        _mover = GetComponent<FireBallMover>();
    }

    private void Start()
    {
        StartCoroutine(GetLifeTimer(_fireBallLifeTimer));
    }

    private void Update()
    {
        if (_fireBallLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator GetLifeTimer(float delay)
    {
        var wait = new WaitForSecondsRealtime(delay);

        while (_fireBallLifeTimer > 0)
        {
            _fireBallLifeTimer--;

            yield return wait;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamageFireBall(gameObject.GetComponent<FireBall>());
            Destroy(gameObject);
        }
    }

    public void GetPlayerLocalScale(Vector2 playerLocalScale)
    {
        _mover.GetPlayerLocalScale(playerLocalScale);
    }
}
