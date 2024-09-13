using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(PlayerMover))]
[RequireComponent (typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _playerMover;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _playerMover.Ran += OnRan;
        _player.Atacked += OnAtacked;
        _playerMover.Jumped += OnJumped;
        _playerMover.CameBackIdle += OnCameBackIdle;
        _player.Hurted += OnHurted;
    }

    private void OnDisable()
    {
        _playerMover.Ran -= OnRan; 
        _player.Atacked -= OnAtacked;
        _playerMover.Jumped -= OnJumped;
        _player.Hurted -= OnHurted;
        _playerMover.CameBackIdle -= OnCameBackIdle; 
    }

    private void OnJumped(bool isJumped)
    {
        _animator.SetBool(Params.IsJumped, isJumped);
    }

    private void OnCameBackIdle(bool isJumped)
    {
        _animator.SetBool(Params.IsIdle, isJumped);
    }

    private void OnRan(bool isJumped)
    {
        _animator.SetBool(Params.IsRun, isJumped);
    }

    private void OnAtacked()
    {
        _animator.SetTrigger(Params.Atacked);
    }

    private void OnHurted()
    {
        _animator.SetTrigger(Params.TakeDamage);
    }

    public static class Params
    {
        public static readonly int IsJumped = Animator.StringToHash(nameof(IsJumped));
        public static readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Atacked = Animator.StringToHash(nameof(Atacked));
        public static readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));
    }
}
