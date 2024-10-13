using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float Value { get; private set; }

    public event Action Dead;
    public event Action ChangedHealth;

    public float CurrentPoint { get; private set; }

    private void Awake()
    {
        CurrentPoint = Value;
    }

    public void TakeHeal(float heal)
    {
        CurrentPoint += heal;

        if (CurrentPoint > Value)
        {
            CurrentPoint = Value;
        }

        ChangedHealth?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        CurrentPoint -= damage;

        if(IsDied())
        {
            Dead?.Invoke();
        }

        ChangedHealth?.Invoke();
    }

    private bool IsDied()
    {
        return CurrentPoint <= 0;
    }
}
