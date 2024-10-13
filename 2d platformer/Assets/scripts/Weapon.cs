using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract float Value { get; }
    public abstract float ReloadTime { get; }
}
