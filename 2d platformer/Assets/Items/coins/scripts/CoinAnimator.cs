using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CoinAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
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

        if (_animationFrame >= _sprites.Length)
            _animationFrame = 0;

        _spriteRenderer.sprite = _sprites[_animationFrame];
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
