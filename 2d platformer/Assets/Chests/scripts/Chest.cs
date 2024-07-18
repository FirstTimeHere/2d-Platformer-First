using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Chest : MonoBehaviour
{
    private BoxCollider2D _chest;

    private void Awake()
    {
        _chest = GetComponent<BoxCollider2D>();
        _chest.isTrigger = true;
    }
}
