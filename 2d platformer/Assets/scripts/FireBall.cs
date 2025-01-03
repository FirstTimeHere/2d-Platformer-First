using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(FireBallAnimation))]
[RequireComponent(typeof(FireBallMover))]
public class FireBall : Ammunition
{
    private float _lifeTimer = 2f;
   
    private FireBallMover _mover;

    public override float Damage { get; protected set;}

    private void Awake()
    {
        _mover = GetComponent<FireBallMover>();
    }

    private void Start()
    {
        StartCoroutine(GetLifeTimer(_lifeTimer));
    }

    private void Update()
    {
        if (_lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator GetLifeTimer(float delay)
    {
        var wait = new WaitForSecondsRealtime(delay);

        while (_lifeTimer > 0)
        {
            _lifeTimer--;

            yield return wait;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(gameObject.GetComponent<Ammunition>());
            Destroy(gameObject);
        }
    }

    public void GetPlayerLocalScale(Vector2 playerLocalScale)
    {
        _mover.GetPlayerLocalScale(playerLocalScale);
    }

    public void SetDamage(float value)
    {
        Damage = value;
    }
}
