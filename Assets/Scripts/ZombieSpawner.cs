using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    private bool _canSpawn;
    [SerializeField] private GameObject _enemy;

    public void StartSpawn()
    {
        StartCoroutine("SpawnEnemy");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnEnemy");
    }

    public void StartBossWaveSpawner()
    {
        Instantiate(_enemy, transform.position, Quaternion.identity);
        //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);
        //GameManager.OnEnemySpawned?.Invoke();
    }

    IEnumerator SpawnEnemy()
    {
        //Control KS pour entourer le code.
        while (_canSpawn)
        {
            float randomTime = Random.Range(2.0f, 5.0f);

            Instantiate(_enemy, transform.position, Quaternion.identity);

            //quaternion.identity pour garder la rotation de l'objet en lui même

            //Instantiate(_spawnParticle.gameObject, transform.position + Vector3.up, _spawnParticle.transform.rotation);

            //int randomClip = Random.Range(0, _screamClip.Length);
            
            //_screamAudio.clip = _screamClip[randomClip];
            //_screamAudio.Play();

            //GameManager.OnEnemySpawned?.Invoke();

            yield return new WaitForSeconds(randomTime);
        }
    }
}
