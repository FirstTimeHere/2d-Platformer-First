using System;
using UnityEngine;

public class RangeVampireMask : MonoBehaviour
{
    public Action<Enemy> DetectedEnemy;
    public Action LostEnemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            DetectedEnemy?.Invoke(enemy);
            Debug.Log("Идет");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            LostEnemy?.Invoke();
            Debug.Log("Остановился");
        }
    }
}
