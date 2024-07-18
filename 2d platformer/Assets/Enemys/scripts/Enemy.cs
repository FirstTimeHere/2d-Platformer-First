using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    public void SetBorders(Border border, Border secondBorder)
    {
        _mover.GetBorders(border, secondBorder);
    }

    public void SetSpeed(float speed)
    {
        _mover.GetSpeed(speed);
    }
}
