using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 新星
public class equip_103 : ItemEffects
{
    private List<Collider2D> enemys;
    private float radiusStart = 0.5f;
    private float radiusEnd = 24f;
    private float radiusChangeRate;
    private float radius;
    private float damage = 30f;

    private Vector2 position;
    public equip_103()
    {
        time = 2f;
        radiusChangeRate = (radiusEnd - radiusStart) / time;
    }


    public override void Run()
    {
        enemys = new List<Collider2D>();
        position = GameObject.Find("Player").transform.position;
        radius = radiusStart;
    }

    public override void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(position, radius);
        radius += Time.deltaTime * radiusChangeRate;
        
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (!enemys.Contains(collider))
                {
                    collider.GetComponent<EnemyControl>().Hitted(damage);
                    enemys.Add(collider);
                }
            }
            else if (collider.CompareTag("EnemyBullet"))
            {
                collider.GetComponent<BulletHelper>().bulletNew.RecycleNow();
            }
        }
    }

    public override void End()
    {
        return;
    }

    public override bool Condition()
    {
        return true;
    }
}
