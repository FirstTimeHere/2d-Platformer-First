using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ChestAnimator))]
public class Chest : MonoBehaviour
{
    private BoxCollider2D _chestCollider;

    private List<Item> _items = new List<Item>();

    public event Action Opened;

    private void Awake()
    {
        _chestCollider = GetComponent<BoxCollider2D>();
        _chestCollider.isTrigger = true;
    }

    private void EnableAllItems()
    {
        foreach (Item item in _items)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void GetOpen()
    {
        Opened?.Invoke();
        EnableAllItems();
    }

    public void DisableCollider()
    {
        _chestCollider.enabled = false;
    }

    public void SetItems(List<Item> items)
    {
        foreach (Item item in items)
        {
            _items.Add(item);
        }
    }
}
