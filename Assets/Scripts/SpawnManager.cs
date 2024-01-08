using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int enemiesInWave = 1;

    public GameObject enemyPrefab;
    public GameObject[] powerUpPrefabs;
    public GameObject rocketPrefab;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, powerUpPrefabs.Length);
        SpawnEnemyWave(enemiesInWave);
        Instantiate(powerUpPrefabs[index], GenerateSpawnPosition(), powerUpPrefabs[index].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        int index = Random.Range(0, powerUpPrefabs.Length);

        enemyCount = FindObjectsOfType<Enemy>().Length;


        if (enemyCount == 0)
        {
            enemiesInWave++;
            SpawnEnemyWave(enemiesInWave);
            Instantiate(powerUpPrefabs[index], GenerateSpawnPosition(), powerUpPrefabs[index].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);

        return spawnPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
