using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ItemMover))]
public class Item : MonoBehaviour
{
    public virtual void GetAbility(Player player)
    {
    }
}
