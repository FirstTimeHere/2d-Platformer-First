using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Border[] _borders;

    public Border[] GetBorders() 
    {
        Border[] borders = _borders;
        return borders; 
    }
}
