using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private float _value;

    public override float Value => _value;
}
