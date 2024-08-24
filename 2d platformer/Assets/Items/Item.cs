using UnityEngine;

[RequireComponent(typeof(ItemMover))]
public abstract class Item : MonoBehaviour
{
    public abstract void GetAbility(Player player);
}
