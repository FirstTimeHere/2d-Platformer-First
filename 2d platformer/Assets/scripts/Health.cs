using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealthPoint;

    public float HealthPoint { get; private set; }
    public float MaxHealthPoint { get; private set; }

    private void Awake()
    {
        HealthPoint = _maxHealthPoint;
        MaxHealthPoint = _maxHealthPoint;
    }
    public void TakeHeal(float heal)
    {
        HealthPoint += heal;

        if (HealthPoint > _maxHealthPoint)
        {
            HealthPoint = _maxHealthPoint;
        }
    }

    public void TakeDamage(float damage)
    {
        HealthPoint -= damage;
    }

    public bool IsDied()
    {
        if (HealthPoint <= 0)
        {
            return true;
        }

        return false;
    }
}
