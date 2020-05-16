using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 锁敌计算机
public class plugin_230 : ItemEffects
{
    private CharacterBulletControl characterBulletControl;
    private Transform playerPositon;
    private Transform targetNow;
    private List<GameObject> enemies;

    private Dictionary<GameObject, Transform> bulletsTarget;

    private float timeGone;
    private readonly float timeScile = 0.7f;
    private float lerpSpeed = 0.01f;
    public plugin_230()
    {
        timeGone = 0f;
    }
    
    public override void Run()
    {
        characterBulletControl = GameObject.Find("Player").GetComponent<CharacterBulletControl>();
        playerPositon = GameObject.Find("Player").GetComponent<Transform>();
        bulletsTarget = new Dictionary<GameObject, Transform>();

        characterBulletControl.OnAddBullet += OnAddBullet;
        characterBulletControl.OnRemoveBullet += OnRemoveBullet;
        
        Debug.Log("230 run");
    }

    public override void Update()
    {
        // 获取距离最近的小怪
        var enemy = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new List<GameObject>(enemy);

        targetNow = GetTarget();
        
        // 追踪
        foreach (var kvp in bulletsTarget)
        {
            if (kvp.Value != null)
            {
                var tmp = kvp.Value.position - kvp.Key.transform.position;
                var lerp = Vector3.Lerp(kvp.Key.transform.up, tmp, lerpSpeed).normalized;
                kvp.Key.transform.up = lerp.normalized;
                kvp.Key.GetComponent<BulletHelper>().bulletNew.dir = lerp.normalized;
            }
        }
    }

    public override void End()
    {
        characterBulletControl.OnAddBullet -= OnAddBullet;
        characterBulletControl.OnRemoveBullet -= OnRemoveBullet;
    }

    public override bool Condition()
    {
        return true;
    }

    private void OnAddBullet(GameObject bullet)
    {
        bulletsTarget.Add(bullet, targetNow);
        bullet.transform.rotation = Quaternion.Euler(Vector3.zero);
        bullet.transform.up = bullet.GetComponent<BulletHelper>().bulletNew.dir.normalized;
    }

    private void OnRemoveBullet(GameObject bullet)
    {
        bulletsTarget.Remove(bullet);
        bullet.transform.up = Vector3.up;
    }

    public Transform GetTarget()
    {
        Transform temp;
        if (enemies.Count == 0)
            return null;
        
        var minDis = Vector3.Distance(playerPositon.position, enemies[0].transform.position);
        temp = enemies[0].transform;
        foreach (var enemy in enemies)
        {
            float dis;
            if ((dis = Vector3.Distance(playerPositon.position, enemy.transform.position)) < minDis)
                temp = enemy.transform;
        }

        return temp;
    }
}
