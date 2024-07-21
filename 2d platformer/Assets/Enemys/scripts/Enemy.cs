using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;

    private float _health;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetBorders(Border border, Border secondBorder)
    {
        _mover.GetBorders(border, secondBorder);
    }

    public void TakeDamageFireBall(FireBall fireBall)
    {
        _health -= fireBall.Damage;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void SetSpeed(float speed)
    {
        _mover.GetSpeed(speed);
    }
}
