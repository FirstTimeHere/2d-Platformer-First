using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(FireBallAnimation))]
public class FireBall : MonoBehaviour
{
    private float _fireBallLifeTimer = 2f;
   
    public float Damage { get; private set; } = 30f;

    private FireBallMover _mover;


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

    public void GetPlayerLocalScale(Vector2 playerLocalScale)
    {
        _mover.GetPlayerLocalScale(playerLocalScale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamageFireBall(gameObject.GetComponent<FireBall>());
            Destroy(gameObject);
        }
    }
}
