using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private int _zombieCount;
    [SerializeField] private int _zombieToSpawn;

    [SerializeField] private float _radius;

    [SerializeField] private Zombie _zombie;

    public float Radius { get => _radius; set => _radius = value; }
    public int ZombieCount { get => _zombieCount; set => _zombieCount = value; }

    private void Start()
    {
        _zombie._zombieState = Zombie.ZombieState.Random;
        
    }

    public void StartSpawn()
    {
        StartCoroutine("SpawnZombie");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnZombie");
    }

    IEnumerator SpawnZombie()
    {
        while (true)
        {
            float randomWalkRadius = Random.Range(10, 20);
            _zombie.WalkRadius = randomWalkRadius;
            //_zombie.Spawner = gameObject.GetComponent<ZombieSpawner>();

            float randomTime = Random.Range(0.5f, 2.0f);
            Vector3 randomPos = new Vector3(Random.Range(-Radius, Radius), 0, Random.Range(-Radius, Radius));

            gameManager.SpawnZombieInSpawner(_zombie, transform.position + randomPos, Quaternion.identity);
            _zombieCount++;


            //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);

            //GameManager.OnEnemySpawned?.Invoke();

            if (_zombieCount >= _zombieToSpawn)
            {
                StopSpawn();
            }

            yield return new WaitForSeconds(randomTime);
        }
    }
}
