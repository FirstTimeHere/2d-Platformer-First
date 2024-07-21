using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FireBall : MonoBehaviour
{
    [SerializeField] private Sprite[] _fireBallAnimation;
    [SerializeField] private float _animationTime;

    private SpriteRenderer _spriteRenderer;

    private FireBallMover _mover;

    private int _animationFrame = 0;

    private float _fireBallLifeTimer = 2f;

    public float Damage { get; private set; } = 30f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mover = GetComponent<FireBallMover>();
    }

    private void Start()
    {
        StartCoroutine(LoopAnimation(_animationTime));
        StartCoroutine(GetLifeTimer(_fireBallLifeTimer));
    }

    private void Update()
    {
        if (_fireBallLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void GetAnimation()
    {
        _animationFrame++;

        if (_animationFrame >= _fireBallAnimation.Length)
            _animationFrame = 0;

        _spriteRenderer.sprite = _fireBallAnimation[_animationFrame];
    }

    private IEnumerator LoopAnimation(float delay)
    {
        var wait = new WaitForSecondsRealtime(delay);

        while (enabled)
        {
            GetAnimation();

            yield return wait;
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
