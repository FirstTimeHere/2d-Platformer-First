using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _points;

    public float CurrentPoint { get; private set; }
    public float MaxPoint { get; private set; }

    private void Awake()
    {
        CurrentPoint = _points;
        MaxPoint = _points;
    }
    public void TakeHeal(float heal)
    {
        CurrentPoint += heal;

        if (CurrentPoint > _points)
        {
            CurrentPoint = _points;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentPoint -= damage;
    }

    public bool IsDied()
    {
        return CurrentPoint <= 0;
    }
}
