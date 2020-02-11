using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTemp : MonoBehaviour
{
    private float spawnTime = 3f;
    private float time = 0;

    //private int EnemyNumber = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnTime) {
            //Debug.Log("生成一个玩意儿");
            LoadEnemy("test");
            time = 0;
        }
    }
    private void LoadEnemy(string enemyNumber) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + "Enemies" + "/" + enemyNumber);
        Instantiate(prefab);
        prefab.transform.position = new Vector2(Random.Range(-5.4f,5.4f), 10.6f);
    }
}
