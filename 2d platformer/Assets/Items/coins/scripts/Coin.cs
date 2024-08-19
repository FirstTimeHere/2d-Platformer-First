using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CoinAnimator))]
public class Coin : Item
{
    [SerializeField] private int _coinValue;

    public override void GetAbility(Player player)
    {
        player.TakeMoney(_coinValue);
    }
}
