using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : BulletNew
{
    public float BoomRange;
    public float subDamage;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {   
        if (isPlayerBullet)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, BoomRange);
            Debug.Log(cols.Length);
            if (cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].transform.CompareTag("Boss") || cols[i].transform.CompareTag("Enemy"))
                    {
                        if (cols[i].gameObject != collision.gameObject)
                            cols[i].gameObject.GetComponent<EnemyControl>().Hitted(subDamage);
                    }
                }
            }
            if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss"))
            {
                collision.gameObject.GetComponent<EnemyControl>().Hitted(Damage);
                HitRecycleNow();
            }
        }
        else if (!isPlayerBullet && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterControl>().DecHP();
            HitRecycleNow();
        }
    }    
}
