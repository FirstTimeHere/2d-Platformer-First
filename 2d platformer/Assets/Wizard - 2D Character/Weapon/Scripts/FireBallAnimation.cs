using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] _fireBallAnimation;
    [SerializeField] private float _animationTime;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame = 0;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(LoopAnimation(_animationTime));
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
}