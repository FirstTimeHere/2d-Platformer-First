using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(Enemy))]
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
        _enemyMover.Ran += OnRun;
        _enemy.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _enemyMover.Ran -= OnRun;
        _enemy.Attacked -= OnAttacked;
    }

    private void OnRun(bool isJumped)
    {
        _animator.SetBool(Params.IsRun, isJumped);
    }

    private void OnAttacked()
    {
        _animator.SetTrigger(Params.Attacked);
    }

    public static class Params
    {
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Attacked = Animator.StringToHash(nameof(Attacked));
    }
}
