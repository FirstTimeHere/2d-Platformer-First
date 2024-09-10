using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;

    private float _health;

    private void Awake()
    {
        _health = _MaxHealth;
    }
    public void TakeHeal(Item item)
    {
        _MaxHealth += item.Value;

        if (_MaxHealth > _health)
        {
            _MaxHealth = _health;
        }
    }

    public void TakeDamage(float damage)
    {
        _MaxHealth -= damage;
    }

    public bool IsDied()
    {
        if (_MaxHealth <= 0)
        {
            return true;
        }

        return false;
    }
}
