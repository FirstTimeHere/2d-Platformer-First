using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<Border> _borders;

    public List<Border> GetBorders()
    {
        return _borders.ToList();
    }
}
