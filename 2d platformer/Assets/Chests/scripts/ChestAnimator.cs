using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Chest))]
public class ChestAnimator : MonoBehaviour
{
    private Animator _animator;
    private Chest _chest;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _chest = GetComponent<Chest>();
    }

    private void OnEnable()
    {
        _chest.Opened += OnOpened;
    }

    private void OnDisable()
    {
        _chest.Opened -= OnOpened;
    }

    private void OnOpened()
    {
        _animator.SetTrigger(Params.IsOpen);
    }

    public static class Params
    {
        public static readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));
    }
}
