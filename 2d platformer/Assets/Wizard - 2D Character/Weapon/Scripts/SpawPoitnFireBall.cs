using UnityEngine;

public class SpawPoitnFireBall : MonoBehaviour
{
    [SerializeField] private FireBall _fireBall;
    [SerializeField] private Player _player;

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

        fireBall.transform.position = transform.position;

        fireBall.GetPlayerLocalScale(_player.GetChildLocalScale());
    }
}
