using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Chest : MonoBehaviour
{
    private BoxCollider2D _chest;

    private Player _player;

    private void Awake()
    {
        _chest = GetComponent<BoxCollider2D>();
        _chest.isTrigger = true;
    }

    public void GetPlayer(Player player)
    {
        _player = player;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == _player.SetCollider() && Input.GetKey(KeyCode.E))
        {
            gameObject.SetActive(false);
        }
    }
}
