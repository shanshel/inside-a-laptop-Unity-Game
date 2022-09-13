using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyPrefabs;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, 1f);
    }

    void Spawn()
    {
        int randPrefabIndex = Random.Range(0, enemyPrefabs.Length);
        Enemy randomPrefab = enemyPrefabs[randPrefabIndex];

        float randomXPos = Random.Range(randomPrefab.minX, randomPrefab.maxX);
        Instantiate(randomPrefab, new Vector3(randomXPos, 5f, 0), Quaternion.identity, null);
    }
}
