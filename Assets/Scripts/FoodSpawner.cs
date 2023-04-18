using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private int _foodCount;

    [SerializeField] private int _maxFoodSpawn;

    [SerializeField] private Food[] _food;

    public void StartSpawn()
    {
        StartCoroutine("SpawnFood");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnFood");
    }

    IEnumerator SpawnFood()
    {
        while (true)
        {
            int randomFood = Random.Range(0, _food.Length);
            Vector3 randomPos = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            gameManager.SpawnFoodInSpawner(_food[randomFood], randomPos, Quaternion.identity);
            gameManager.Instance().FoodCount++;

            //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);

            //GameManager.OnEnemySpawned?.Invoke();

            if (gameManager.Instance().FoodCount >= gameManager.Instance().MaxFoodSpawn)
            {
                StopSpawn();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
