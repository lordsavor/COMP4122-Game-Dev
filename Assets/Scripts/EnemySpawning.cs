using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    [SerializeField] public GameObject Enemy;
    [SerializeField] public int MaximumEnemy = 10;

    public int enemyCount;
    private int BasexPos;
    private int BasezPos;

    // Start is called before the first frame update
    void Start()
    {
        BasexPos = (int)transform.position.x;
        BasezPos = (int)transform.position.z;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCount < MaximumEnemy)
        {
            int xPos = BasexPos + Random.Range(1, 50);
            int zPos = BasezPos + Random.Range(1, 50);
            Instantiate(Enemy, new Vector3(xPos, transform.position.y, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount++;
        }
    }
}