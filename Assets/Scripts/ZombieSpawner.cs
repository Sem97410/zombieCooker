using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.PlayerLoop;

public class ZombieSpawner : Spawner
{
    [SerializeField] Zombie[] zombie;

    protected override IEnumerator Spawn()
    {
        while (true)
        {
            float randomTime = Random.Range(2.0f, 5.0f);
            int randomGo = Random.Range(0, zombie.Length);

            yield return new WaitForSeconds(randomTime);

            Vector3 pos = transform.position + new Vector3(0, 50, 0) + new Vector3(Random.Range(-Radius, Radius), 0, Random.Range(-Radius, Radius));

            Ray ray = new Ray(pos, Vector3.down);
            Debug.DrawRay(ray.origin, ray.direction * 150, Color.red);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 150))
            {
                zombie[randomGo]._zombieState = Zombie.ZombieState.Random;
                zombie[randomGo].Spawner = this;
                gameManager.SpawnGoInSpawner(zombie[randomGo], hit.point, Quaternion.identity);
                Count++;
            }

            //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);

            //GameManager.OnEnemySpawned?.Invoke();

            if (Count >= NumberToSpawn)
            {
                StopSpawn();
            }
            
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
