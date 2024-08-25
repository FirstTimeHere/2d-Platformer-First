using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CoinAnimator))]
public class Coin : Item
{
    [SerializeField] private int _coinValue;

    public override float Value => _coinValue;
}
