using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://discussions.unity.com/t/instantiate-prefab-from-script-inside-a-canvas/170628
public class EnemyHitboxManager : MonoBehaviour
{
    public GameObject hitbox;
    
    public void Awake()
    {
        GameObject[] allEnemies;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        for (int i = 0; i < allEnemies.Length; i++) {
            GameObject hitboxInstance = Instantiate(hitbox, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            hitboxInstance.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform, false);
            hitboxInstance.GetComponent<HitboxFollowEnemy>().enemyLookAt = allEnemies[i].transform;
            hitboxInstance.GetComponent<HitboxFollowEnemy>().target = allEnemies[i];
        }
    }


}
