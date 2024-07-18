using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPointChest> _spawnPoints;
    [SerializeField] private Chest[] _chestsType;
    [SerializeField] private Player _player;

    private void Spawn()
    {
        List<SpawnPointChest> tempPoints = new List<SpawnPointChest>();

        foreach (SpawnPointChest spawnPoint in _spawnPoints)
        {
            tempPoints.Add(spawnPoint);
        }

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            int randomPoint = Random.Range(0, tempPoints.Count);
            int randomChest = Random.Range(0, _chestsType.Length);

            Chest chest = Instantiate(_chestsType[randomChest]);
            chest.transform.position = tempPoints[randomPoint].transform.position;

            tempPoints.RemoveAt(randomPoint);
        }
    }

    private void Awake()
    {
        Spawn();
    }
}
