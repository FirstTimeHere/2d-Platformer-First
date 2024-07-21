using UnityEngine;

[RequireComponent (typeof(Animator))]
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
        _playerMover.Ran += Run;
        _player.Atacked += Atacked;
        _playerMover.Jumped += Jump;
        _playerMover.CameBackIdle += Idle;

    }

    private void OnDisable()
    {
        _playerMover.Ran -= Run; 
        _player.Atacked -= Atacked;
        _playerMover.Jumped -= Jump;
        _playerMover.CameBackIdle -= Idle; 
    }

    private void Jump(bool isJumped)
    {
        _animator.SetBool(Params.IsJumped, isJumped);
    }

    private void Idle(bool isJumped)
    {
        _animator.SetBool(Params.IsIdle, isJumped);
    }

    private void Run(bool isJumped)
    {
        _animator.SetBool(Params.IsRun, isJumped);
    }

    private void Atacked()
    {
        _animator.SetTrigger(Params.Atacked);
    }

    public static class Params
    {
        public static readonly int IsJumped = Animator.StringToHash(nameof(IsJumped));
        public static readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Atacked = Animator.StringToHash(nameof(Atacked));
    }
}
