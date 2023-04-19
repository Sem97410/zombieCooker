using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        ZombieEvents.onZombieSpawnedDied += RespawnZombieDied;

    }

    public void StartSpawn()
    {
        StartCoroutine("SpawnZombie");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnZombie");
    }

    void RespawnZombieDied()
    {
        ZombieCount--;
        StartSpawn();
    }

    IEnumerator SpawnZombie()
    {
        while (true)
        {
            float randomTime = Random.Range(2.0f, 5.0f);

            yield return new WaitForSeconds(randomTime);
            
            float randomWalkRadius = Random.Range(10, 20);
            _zombie.WalkRadius = randomWalkRadius;
            _zombie.Spawner = this;

            Vector3 pos = transform.position + new Vector3(0, 50, 0) + new Vector3(Random.Range(-Radius, Radius), 0, Random.Range(-Radius, Radius));

            Ray ray = new Ray(pos, Vector3.down);
            Debug.DrawRay(ray.origin, ray.direction * 150, Color.red);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 150))
            {
                gameManager.SpawnZombieInSpawner(_zombie, hit.point, Quaternion.identity);
                _zombieCount++;
            }

            //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);

            //GameManager.OnEnemySpawned?.Invoke();

            if (_zombieCount >= _zombieToSpawn)
            {
                StopSpawn();
            }

        }
    }

}
