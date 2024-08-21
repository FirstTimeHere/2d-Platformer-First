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

    public void GetOpen()
    {
        Opened?.Invoke();
        EnableAllItems();
    }

    public void DisableCollider(Chest chest)
    {
        BoxCollider2D collider = chest.GetComponent<BoxCollider2D>();
        collider.enabled = false;
    }

    public void GetItemsFromSpawnner(List<Item> items)
    {
        foreach (Item item in items)
        {
            _items.Add(item);
        }
    }

    private void EnableAllItems()
    {
        foreach (Item item in _items)
        {
            item.gameObject.SetActive(true);
        }
    }
}
