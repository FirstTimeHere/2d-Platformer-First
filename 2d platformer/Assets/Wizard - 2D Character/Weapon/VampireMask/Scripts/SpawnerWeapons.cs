using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeapons : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Weapon[] _weapons;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        Weapon weapon = Instantiate(_weapons[0]);
        weapon.transform.position = _spawnPoints[0].transform.position;
    }
}
