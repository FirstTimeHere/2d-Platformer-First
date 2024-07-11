using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.GetJump += Jump;
        _playerMover.GetIdle += Idle;
        _playerMover.GetRun += Run;
    }

    private void OnDisable()
    {
        _playerMover.GetJump -= Jump;
        _playerMover.GetIdle -= Idle; 
        _playerMover.GetRun -= Run; 
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

    public static class Params
    {
        public static readonly int IsJumped = Animator.StringToHash(nameof(IsJumped));
        public static readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
    }
}
