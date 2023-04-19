using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int _count;

    [SerializeField] private int _numberToSpawn;

    [SerializeField] private float _radius;

    public int Count { get => _count; set => _count = value; }
    public int NumberToSpawn { get => _numberToSpawn; set => _numberToSpawn = value; }
    public float Radius { get => _radius; set => _radius = value; }

    public void StartSpawn()
    {
        StartCoroutine("Spawn");
    }

    public void StopSpawn()
    {
        StopCoroutine("Spawn");
    }

    protected virtual IEnumerator Spawn()
    {
        while (true)
        {
           
        }
    }
}
