using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField, Range(0f, 10f)] private float _speed;
    [SerializeField] private Enemy _enemy;

    private List<Enemy> _enemies=new List<Enemy>();

    private int _enemyCount = 0;

    private void Awake()
    {
        _enemyCount = _spawnPoints.Length;

        for (int i = 0; i < 10; i++)
        {
            _enemies.Add(_enemy);
        }
    }

    private void Spawn()
    {
        while (_enemyCount > 0)
        {
            Border[] borders = _spawnPoints[_enemyCount-1].GetBorders();

            Enemy enemy = Instantiate(_enemies[_enemies.Count-1]);
            enemy.gameObject.SetActive(true);
            enemy.GetBorders(borders[0], borders[1]);
            enemy.transform.position = _spawnPoints[_enemyCount-1].transform.position;

            _enemyCount--;
        }
    }

    private void Update()
    {
        Spawn();
    }
}
