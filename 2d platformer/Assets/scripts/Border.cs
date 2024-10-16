using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : MonoBehaviour
{
    public BoxCollider2D Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
    }
}
