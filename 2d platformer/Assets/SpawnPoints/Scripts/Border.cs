using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : MonoBehaviour
{
    public BoxCollider2D ColliderBorder { get; private set; }

    private void Awake()
    {
        ColliderBorder = GetComponent<BoxCollider2D>();
    }
}
