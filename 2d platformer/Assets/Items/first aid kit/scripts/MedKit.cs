using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private float _healthPoint;

    public override float Value => _healthPoint;
}
