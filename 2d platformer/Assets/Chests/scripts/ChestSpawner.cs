using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPointChest> _spawnPoints;
    [SerializeField] private Chest[] _chestsType;
    [SerializeField] private Player _player;
    [SerializeField] private Item[] _items;

    private int _itemsInChest = 10;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        List<SpawnPointChest> tempPoints = new List<SpawnPointChest>();
        List<Item> tempItems = new List<Item>();

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

            for (int j = 0; j < _itemsInChest; j++)
            {
                int randomItem = Random.Range(0, _items.Length);
                Item item = Instantiate(_items[randomItem], chest.transform); 
                item.gameObject.SetActive(false);
                tempItems.Add(item);
            }

            chest.SetItems(tempItems);
            tempItems.Clear();

            tempPoints.RemoveAt(randomPoint);
        }
    }
}
