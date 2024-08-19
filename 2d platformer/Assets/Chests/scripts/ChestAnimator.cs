using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
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
        _chest.Opened += Open;
    }

    private void OnDisable()
    {
        _chest.Opened -= Open;
    }

    private void Open()
    {
        _animator.SetTrigger(Params.IsOpen);
    }

    public static class Params
    {
        public static readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));
    }
}
