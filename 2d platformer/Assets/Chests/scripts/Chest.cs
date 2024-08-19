using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ChestAnimator))]
public class Chest : MonoBehaviour
{
    private BoxCollider2D _chest;

    public event Action Opened;

    private void Awake()
    {
        _chest = GetComponent<BoxCollider2D>();
        _chest.isTrigger = true;
    }

    public void GetOpen()
    {
        Opened?.Invoke();
    }

    public void DisableCollider(Chest chest)
    {
        BoxCollider2D collider = chest.GetComponent<BoxCollider2D>();
        collider.enabled = false;
    }
}
