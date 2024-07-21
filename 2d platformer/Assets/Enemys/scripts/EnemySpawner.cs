using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;

    [SerializeField] private Enemy _enemy;

    [SerializeField, Range(0f, 5f)] private float _speed;

    private List<Enemy> _enemies = new List<Enemy>();

    private int _enemyCount = 0;

    private void Awake()
    {
        _enemyCount = _spawnPoints.Length;

        for (int i = 0; i < _enemyCount; i++)
        {
            _enemies.Add(_enemy);
        }
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = _enemyCount; i > 0; i--)
        {
            List<Border> borders = _spawnPoints[i - 1].GetBorders();

            Enemy enemy = Instantiate(_enemies[_enemies.Count - 1]);
            enemy.transform.position = _spawnPoints[i - 1].transform.position;

            enemy.SetBorders(borders[0], borders[1]);
            enemy.SetSpeed(_speed);
            enemy.SetHealth(GetRandomHealth());
        }
    }

    private float GetRandomHealth()
    {
        int minRandomValue = 0;
        int maxRandomValue = 1001;

        float randomValue = Random.Range(minRandomValue, maxRandomValue);

        return randomValue;
    }
}
