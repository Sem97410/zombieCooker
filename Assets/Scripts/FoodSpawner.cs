using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private int _foodCount;

    [SerializeField] private int _foodToSpawn;

    [SerializeField] private float _radius;

    [SerializeField] private Food[] _food;

    public int FoodCount { get => _foodCount; set => _foodCount = value; }
    public int FoodToSpawn { get => _foodToSpawn; set => _foodToSpawn = value; }
    public float Radius { get => _radius; set => _radius = value; }

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
            Vector3 randomPos = new Vector3(Random.Range(-Radius, Radius), 0, Random.Range(-Radius, Radius));
            gameManager.SpawnFoodInSpawner(_food[randomFood], transform.position + randomPos, Quaternion.identity);
            FoodCount++;

            //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);

            //GameManager.OnEnemySpawned?.Invoke();

            if (FoodCount >= FoodToSpawn)
            {
                StopSpawn();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
