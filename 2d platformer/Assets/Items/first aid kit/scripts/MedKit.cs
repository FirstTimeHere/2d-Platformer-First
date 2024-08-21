using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private float _healthPoint;

    public override void GetAbility(Player player)
    {
        player.TakeHeal(_healthPoint);
    }
}
