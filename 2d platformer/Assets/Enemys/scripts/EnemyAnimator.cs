using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _enemyMover.GetJump += Jump;
        _enemyMover.GetIdle += Idle;
        _enemyMover.GetRun += Run;
    }

    private void OnDisable()
    {
        _enemyMover.GetJump -= Jump;
        _enemyMover.GetIdle -= Idle;
        _enemyMover.GetRun -= Run;
    }

    private void Jump(bool isJumped)
    {
        _animator.SetBool(Params.IsJumped, isJumped);
    }

    private void Idle(bool isJumped)
    {
        _animator.SetTrigger(Animator.StringToHash(nameof(Idle)));
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
