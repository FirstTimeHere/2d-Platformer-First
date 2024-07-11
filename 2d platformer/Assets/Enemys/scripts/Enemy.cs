using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    public void GetBorders(Border border,Border secondBorder)
    {
        _mover.GetBorders(border, secondBorder);
    }
}
