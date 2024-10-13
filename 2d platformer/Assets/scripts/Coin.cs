using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CoinAnimator))]
public class Coin : Item
{
    [SerializeField] private int _value;

    public override float Value => _value;
}
