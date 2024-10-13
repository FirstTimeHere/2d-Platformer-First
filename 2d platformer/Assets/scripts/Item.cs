using UnityEngine;

[RequireComponent(typeof(ItemMover))]
public abstract class Item : MonoBehaviour
{
    public abstract float Value { get; }
}
