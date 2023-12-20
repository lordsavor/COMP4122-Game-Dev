using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public int maximumEnemy = 10;
    [SerializeField] public Transform targetPoint;

    public int enemyCount;
    private int baseXPos;
    private int baseZPos;

    // Start is called before the first frame update
    void Awake()
    {
        baseXPos = (int)transform.position.x;
        baseZPos = (int)transform.position.z;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCount < maximumEnemy)
        {
            int xPos = baseXPos + Random.Range(1, 50);
            int zPos = baseZPos + Random.Range(1, 50);
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(xPos, transform.position.y, zPos), Quaternion.identity);

            // Get the appropriate script component from the spawned enemy
            EnemyAI enemyAI = enemy.GetComponentInChildren<EnemyAI>();
            AirEnemyAI airEnemyAI = enemy.GetComponentInChildren<AirEnemyAI>();

            if (enemyAI != null)
            {
                // Update the targetPosition variable in EnemyAI script
                enemyAI.Checkpoint = targetPoint;
                Debug.Log("1");
            }
            if (airEnemyAI != null)
            {
                // Update the targetPosition variable in AirEnemyAI script
                airEnemyAI.Checkpoint = targetPoint;
                Debug.Log("2");
            }

            yield return new WaitForSeconds(0.1f);
            enemyCount++;
        }
    }
}