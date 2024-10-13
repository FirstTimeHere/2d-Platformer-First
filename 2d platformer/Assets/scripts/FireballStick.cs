using UnityEngine;

public class FireballStick : Weapon
{
    [SerializeField] private FireBall _fireBall;
    [SerializeField] private Player _player;

    [SerializeField] private Transform _spawnPointFireball;

    [SerializeField] private float _valueFireBall;

    public override float Value => _valueFireBall;
    public override float ReloadTime { get; }

    private void OnEnable()
    {
        _player.Atacked += Spawn;
    }

    private void OnDisable()
    {
        _player.Atacked -= Spawn;
    }

    private void Spawn()
    {
        FireBall fireBall = Instantiate(_fireBall);

        fireBall.SetDamage(Value);

        fireBall.transform.position = _spawnPointFireball.position;

        fireBall.GetPlayerLocalScale(_player.GetChildLocalScale());
    }
}
