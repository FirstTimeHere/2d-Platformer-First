using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private EnemyMover _enemyMover;
    private Enemy _enemy;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemyMover.Ran += Run;
        _enemy.Attacked += Attacked;
    }

    private void OnDisable()
    {
        _enemyMover.Ran -= Run;
        _enemy.Attacked -= Attacked;
    }

    private void Run(bool isJumped)
    {
        _animator.SetBool(Params.IsRun, isJumped);
    }

    private void Attacked()
    {
        _animator.SetTrigger(Params.Attacked);
    }

    public static class Params
    {
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Attacked = Animator.StringToHash(nameof(Attacked));
    }
}
