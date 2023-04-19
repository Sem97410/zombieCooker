using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Zombie _zombie;

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
            float randomWalkRadius = Random.Range(5, 15);
            _zombie.WalkRadius = randomWalkRadius;

            float randomTime = Random.Range(0.5f, 2.0f);
            Vector3 randomPos = new Vector3(Random.Range(-30, 30),0, Random.Range(-30, 30));
            gameManager.SpawnZombieInSpawner(_zombie, transform.position + randomPos, Quaternion.identity);
            gameManager.Instance().ZombieCount++;

            //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);

            //GameManager.OnEnemySpawned?.Invoke();

            if (gameManager.Instance().ZombieCount >= gameManager.Instance().MaxZombieSpawn)
            {
                StopSpawn();
            }

            yield return new WaitForSeconds(randomTime);
        }
    }
}
