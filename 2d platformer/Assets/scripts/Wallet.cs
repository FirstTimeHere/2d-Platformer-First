using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public float Money {  get; private set; }

    public void TakeMoney(Coin coin)
    {
        Money += coin.Value;
    }
}
